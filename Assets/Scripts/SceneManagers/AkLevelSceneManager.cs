using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.SceneManagement;

public class AkLevelSceneManager : MonoBehaviour
{
    [SerializeField] MubDialogues dialogues;
    [SerializeField] TeacherController teacherController;
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject MatchReadyBox;
    [SerializeField] TMP_Text countDownText, readyText;
    [SerializeField] AKLevelMatchManager matchManager;
    [SerializeField] PlayableDirector fadeOut;


    int currentCount = 3;

    private void Start()
    {
        StartCoroutine("MubEntry");
    }

    IEnumerator MubEntry()
    {
        Debug.Log("Entry");
        yield return new WaitForSeconds(3f);
        dialogues.TriggerAKEntryDialogueDialogue();

        while (!dialogues.AKEntryDialogue.dialogueFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        teacherController.setTeacherStateReady(true); //Mub ready
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
            yield return new WaitForSeconds(1f);
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
    public void fadeSceneOut(bool win)
    {
        fadeOut.Play();

        if (win && PlayerPrefs.GetInt("currentScene", 0) < 3)
        {
            PlayerPrefs.SetInt("currentScene", 3);
            Debug.Log("Current scene prefs set to 3");
        }

        StartCoroutine("CfadeSceneOut");
    }

    IEnumerator CfadeSceneOut()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("LevelSelector");
    }
}
