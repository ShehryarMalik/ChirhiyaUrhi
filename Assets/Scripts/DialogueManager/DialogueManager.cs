using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Dialogue currentDialogue;

    public TMP_Text nameText, dialogueText;
    public GameObject avatarImage;

    [SerializeField] private float textSpeed;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private AudioSource openingSFX , closingSFX, nextSFX, DefaultTypeNote, currentTypeNote;
    private string[] sentencess;
    private string[] speakerNames;
    private Sprite[] avatars;
    private AudioSource[] typeNotes;
    private int numberOfSentences;
    private int currentSentenceIndex;


    void Start()
    {
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (openingSFX)
            openingSFX.Play();

        dialogue.dialogueStarted = true;
        currentDialogue = dialogue;
        dialogueBox.SetActive(true);
        currentSentenceIndex = 0;
        numberOfSentences = dialogue.sentenceStruct.Length;

        sentencess = new string[numberOfSentences];
        speakerNames = new string[numberOfSentences];
        avatars = new Sprite[numberOfSentences];
        typeNotes = new AudioSource[numberOfSentences];

        for(int i = 0; i < numberOfSentences; i++)
        {
            speakerNames[i] = dialogue.sentenceStruct[i].speakerName;
            sentencess[i] = dialogue.sentenceStruct[i].sentence;
            avatars[i] = dialogue.sentenceStruct[i].avatarImage;
            typeNotes[i] = dialogue.sentenceStruct[i].TypeNoteSFX;
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(currentSentenceIndex >= numberOfSentences)
        {
            EndDialogue();
            return;
        }

        if(nextSFX)
            nextSFX.Play();

        nameText.text = speakerNames[currentSentenceIndex];
        if(avatars[currentSentenceIndex] == null)
        {
            avatarImage.SetActive(false);
        }
        else
        {
            avatarImage.GetComponent<Image>().sprite = avatars[currentSentenceIndex];
            avatarImage.SetActive(true);
        }
        
        if(typeNotes[currentSentenceIndex] == null)
        {
            if(DefaultTypeNote)
            currentTypeNote = DefaultTypeNote;
        }
        else
        {
            currentTypeNote = typeNotes[currentSentenceIndex];
        }

        StopAllCoroutines();

        StartCoroutine(TypeSentence(sentencess[currentSentenceIndex]));
        currentSentenceIndex++;
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            //voice
            if(currentTypeNote)
            {
                currentTypeNote.pitch = Random.Range(0.8f, 1.1f);
                currentTypeNote.Play();
            }
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void EndDialogue()
    {
        Debug.Log("End of sentences");
        dialogueBox.GetComponent<DialogueBox>().close();
        if(currentDialogue != null)
        currentDialogue.dialogueFinished = true;
        currentDialogue = null;
        if (openingSFX)
            closingSFX.Play();
    }

}
