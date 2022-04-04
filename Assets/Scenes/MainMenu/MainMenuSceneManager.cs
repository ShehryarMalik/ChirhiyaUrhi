using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class MainMenuSceneManager : MonoBehaviour
{
    [SerializeField] MainMenuDialogues dialogues;
    [SerializeField] PlayableDirector fadeOut;

    private void Start()
    {
        //PlayerPrefs.SetInt("currentScene", 1);
    }

    public void checkNewPlayer()
    {
        if (PlayerPrefs.GetInt("currentScene", 0) < 1)
        {
            newPlayerTrigger();
        }
        else
        {
            StartCoroutine("loadLevelSelector");
        }
    }

    public void newPlayerTrigger()
    {
        StartCoroutine("newPlayerTrack");
    }

    IEnumerator newPlayerTrack()
    {
        yield return new WaitForSeconds(1f);
        dialogues.TriggerNewPlayerDialogue();

        while(!dialogues.NewPlayerDialogue.dialogueFinished)
        {
            yield return null;
        }

        //After Dialogue fade out and load scene
        fadeOut.Play();
        yield return new WaitForSeconds(4f);
        loadTutorialScene();
    }

    IEnumerator loadLevelSelector()
    {
        Debug.Log("In load level selctor ()");
        fadeOut.Play();
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("LevelSelector");
    }

    public void loadTutorialScene()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    public void deletePlayerData()
    {
        PlayerPrefs.DeleteAll();
    }
}
