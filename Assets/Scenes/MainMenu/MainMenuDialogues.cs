using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuDialogues : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public Dialogue NewPlayerDialogue;

    public void TriggerNewPlayerDialogue()
    {
        dialogueManager.StartDialogue(NewPlayerDialogue);
    }
}
