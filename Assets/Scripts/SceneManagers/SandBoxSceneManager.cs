using UnityEngine;
using UnityEngine.Playables;
using System.Collections;
using TMPro;
public class SandBoxSceneManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector teacherEntry;
    [SerializeField] SandboxDialogues sandBoxDialogues;
    [SerializeField] TeacherController teacherController;
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject MatchReadyBox;
    [SerializeField] MatchManager matchManager;

    [SerializeField] TMP_Text countDownText, readyText;
    int currentCount = 3;


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
            yield return new WaitForSeconds(1.5f);
            currentCount--;
            countDownText.text = currentCount.ToString();
            yield return null;
        }

        //pass
        if (currentCount == 0 && playerController.getPlayerStateDown())
        {
            MatchReadyBox.SetActive(false);
            matchManager.StartRoundMain(); //start Match's phase 1
        } 
        
        //fail
        else
        {
            countDownText.gameObject.SetActive(false);
            readyText.gameObject.SetActive(true);
            StartCoroutine("IsFingerDown");
        }
    }
}
