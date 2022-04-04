using UnityEngine;
using UnityEngine.Playables;
using System.Collections;
using TMPro;
public class TutorialSceneManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector teacherEntry, sceneFadeOutPlayable;
    [SerializeField] SandboxDialogues sandBoxDialogues;
    [SerializeField] TeacherController teacherController;
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject MatchReadyBox;
    [SerializeField] MatchManager matchManager;

    [SerializeField] TMP_Text countDownText, readyText;

    [SerializeField] GameObject tutorial1;
    int currentCount = 3;

    [SerializeField] GameObject healthCanvas;

    private void Start()
    {
        healthCanvas.SetActive(false);
        startTeacherEntry();
        //matchManager.StartPhase2();
    }

    public void startTeacherEntry()
    {
        teacherEntry.Play();
    }

    public void triggerTeacherEntryDialogue()
    {
        //stop the cinematic
        teacherEntry.Stop();
        sandBoxDialogues.TriggerTeacherEntryDialogue();
        StartCoroutine("TeacherEntryDialogueBreak");
    }
    IEnumerator TeacherEntryDialogueBreak()
    {

        while (!sandBoxDialogues.teacherEntryDialogue.dialogueFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        
        //trigger tutorial 1
        tutorial1.SetActive(true);

    }

    public void closeTutorial1()
    {
        StartCoroutine("readyMatch");
    }

    IEnumerator readyMatch()
    {
        teacherController.setTeacherStateReady(true); //teacher ready
        playerController.setPlayerStateReady(true); //player ready
        yield return new WaitForSeconds(0.5f);
        teacherController.setTeacherStateDown(true);
        Debug.Log("Match ready phase");
        MatchReadyBox.SetActive(true);

        StartCoroutine("IsFingerDown");
    }

    IEnumerator IsFingerDown()
    {
        MatchReadyBox.SetActive(true);

        //if player is not touching 
        while (!playerController.getPlayerStateDown())
        {
            yield return null;
        }

        //Player is holding touch
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("CountDownMain");
    }

    IEnumerator CountDownMain()
    {
        currentCount = 3;
        countDownText.gameObject.SetActive(true);
        readyText.gameObject.SetActive(false);
        countDownText.text = currentCount.ToString();

        while (playerController.getPlayerStateDown() && currentCount > 0)
        {
            yield return new WaitForSeconds(0.5f);
            currentCount--;
            countDownText.text = currentCount.ToString();
            yield return null;
        }

        //pass
        if (currentCount == 0 && playerController.getPlayerStateDown())
        {
            MatchReadyBox.SetActive(false);
            matchManager.StartRoundMain(); //start round loop
        } 
        
        //fail
        else
        {
            countDownText.gameObject.SetActive(false);
            readyText.gameObject.SetActive(true);
            StartCoroutine("IsFingerDown");
        }
    }

    public void fadeSceneOut()
    {
        sceneFadeOutPlayable.Play();
    }

}
