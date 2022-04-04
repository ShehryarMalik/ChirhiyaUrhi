using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour
{
    [SerializeField] TeacherController teacherController;
    [SerializeField] PlayerController playerController;

    [SerializeField] bool fakeHand = false;
    [SerializeField] bool roundLoop = false, roundWin = false;
    [SerializeField] bool checkPlayerMoves = false;
    [SerializeField] bool previousState = false, currentState = false;

    [SerializeField] float reactionTime = 2f;
    [SerializeField] float roundDelay = 2f;
    [SerializeField] float downCheckDelay = 0.5f;

    [SerializeField] GameObject correctImageGO, wrongImageGO;
    [SerializeField] AudioSource correctSFX, wrongSFX;

    [SerializeField] TMP_Text roundNumberText;
    [SerializeField] GameObject roundNumberGO;
    [SerializeField] GameObject Phase2Tutorial;

    [SerializeField] HealthSystem healthSystem;
    [SerializeField] GameObject healthCanvas;
    [SerializeField] SandboxDialogues sandBoxDialogues;
    [SerializeField] TutorialSceneManager sceneManager;

    int currentPhase = 1;

    public void StartRoundMain()
    {
        roundLoop = true;
        StartCoroutine("MatchPhase1");
    }

    IEnumerator MatchPhase1()
    {
        int currentRound = 1;
        currentPhase = 1;
        roundNumberGO.SetActive(true);
        //when roundloop is turned on


        //Phase 1
        while (currentRound <= 5)
        {
            yield return new WaitForSeconds(roundDelay); // round delay


            teacherController.chooseAWord();    //teacher selects a word
            teacherController.speakWord();      //plays the audio cue

            //if fakehand is disabled, raise hand when necessary, if it is enabled
            //then randomly raise hand
            if(!fakeHand)
            {
                if (teacherController.canWordFly())
                {
                    teacherController.raiseHandOnce();
                }
            }
            else
            {
                //randomly raise hand
                bool shouldRaise = randomBool();

                if(shouldRaise)
                teacherController.raiseHandOnce();
            }


            //2 second delay after issuing command
            yield return new WaitForSeconds(reactionTime); //reaction time

            roundWin = CheckPlayerMoves();
            
            //if correct move was made
            if(roundWin)
            {
                Debug.Log("Correcto");
                correctSFX.Play();
                correctImageGO.SetActive(true);

                if (roundNumberText)
                {
                    currentRound++;
                    roundNumberText.text = currentRound.ToString();
                }
            }
            else
            {
                Debug.Log("Wrong");
                wrongSFX.Play();
                wrongImageGO.SetActive(true);
            }
        }

        Debug.Log("Phase 1 Complete");
        roundNumberGO.SetActive(false);
        teacherController.setTeacherStateDown(false);
        teacherController.setTeacherStateUp(false);
        teacherController.setTeacherStateReady(false);

        playerController.setPlayerStateReady(false);
        yield return new WaitForSeconds(1f);
        Phase2Tutorial.SetActive(true);
    }

    //end of phase 1

    //phase 2
    public void StartPhase2()
    {
        roundLoop = true;
        healthCanvas.SetActive(true);
        playerController.enableSparks = true;
        sandBoxDialogues.matchWinDialogue.dialogueFinished = false;
        sandBoxDialogues.matchLoseDialogue.dialogueFinished = false;
        roundDelay = 2f;
        fakeHand = false;
        StartCoroutine("MatchPhase2");
    }

    IEnumerator MatchPhase2()
    {
        int currentRound = 1;
        currentPhase = 2;
        healthSystem.setStamina(0);
        healthSystem.setHealth(100);
        healthSystem.setOpHealth(100);
        healthSystem.staminaHitDamage = 50f; //teacher loses with 2 stamina hits

        //when roundloop is turned on
        Debug.Log("Phase 2 Start");
        teacherController.setTeacherStateReady(true);
        playerController.setPlayerStateReady(true);

        while (healthSystem.getPlayerHealth() > 0 && healthSystem.getOpHealth() > 0)
        {
            //condition 1 - Teacher speeds up after her health is reduced to 60
            //condition 2 - starts fake hand

            if (healthSystem.getOpHealth() <= 90f)
            {
                roundDelay = 1f;
                fakeHand = true;
            }

            yield return new WaitForSeconds(roundDelay); // round delay 2 at first

            healthSystem.enableStamina = true;
            teacherController.chooseAWord();    //teacher selects a word
            teacherController.speakWord();      //plays the audio cue

            //if fakehand is disabled, raise hand when necessary, if it is enabled
            //then randomly raise hand
            if (!fakeHand)
            {
                if (teacherController.canWordFly())
                {
                    teacherController.raiseHandOnce();
                }
            }
            else
            {
                //randomly raise hand
                bool shouldRaise = randomBool();

                if (shouldRaise)
                    teacherController.raiseHandOnce();
            }


            //2 second delay after issuing command
            yield return new WaitForSeconds(reactionTime); //reaction time

            roundWin = CheckPlayerMoves();

            //if correct move was made
            if (roundWin)
            {
                Debug.Log("Correcto");
                correctSFX.Play();
                correctImageGO.SetActive(true);

                if (roundNumberText)
                {
                    currentRound++;
                    roundNumberText.text = currentRound.ToString();
                }
            }
            else
            {
                Debug.Log("Wrong");
                wrongSFX.Play();
                wrongImageGO.SetActive(true);
                healthSystem.DecreaseHealth(34f); //so it is 0 in 3 hits
            }
        }

        Debug.Log("Phase 2 Complete");

        teacherController.setTeacherStateDown(false);
        teacherController.setTeacherStateUp(false);
        teacherController.setTeacherStateReady(false);

        playerController.setPlayerStateReady(false);
        playerController.enableSparks = false;
        playerController.StopSparks();
        healthCanvas.SetActive(false);

        yield return new WaitForSeconds(1f);

        if (healthSystem.getOpHealth() <= 0)
        {
            //won match
            Debug.Log("You win");
            sandBoxDialogues.TriggerMatchWinDialogue();
            StartCoroutine("WinMatchBreak");
        }

        else
        {
            //lost match
            Debug.Log("You Lost");
            sandBoxDialogues.TriggerMatchLoseDialogue();
            StartCoroutine("LoseMatchBreak");

        }
    }
    //end of phase 2

    IEnumerator WinMatchBreak()
    {

        while (!sandBoxDialogues.matchWinDialogue.dialogueFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        Debug.Log("Level Finished");
        sceneManager.fadeSceneOut();

        if(PlayerPrefs.GetInt("currentScene", 0) < 1)
            PlayerPrefs.SetInt("currentScene", 1);

        SceneManager.LoadScene("LevelSelector");
    }

    IEnumerator LoseMatchBreak()
    {

        while (!sandBoxDialogues.matchLoseDialogue.dialogueFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        StartPhase2();
    }

    bool CheckPlayerMoves()
    {
            previousState = playerController.getPlayerStateDown();
            currentState = previousState;

            //Check if the currently selected word can fly
            bool correctState = !teacherController.canWordFly();

            //correct state: 0 when thing cant fly, 1 when it can
            //0 when hand is up, 1 when hand is down

            //if current state is correct state
            if(currentState == correctState)
            {
                return true;

            }
            else
            {
                return false;
            }

        }

    bool randomBool()
    {
        return (Random.value > 0.5f);
    }
}