using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogueTest1;
    public Dialogue dialogueTest2;

    public DialogueManager dialogueManager;

    public void TriggerDialogueTest1()
    {
        dialogueManager.StartDialogue(dialogueTest1);
    }

    public void TriggerDialogueTest2()
    {
        dialogueManager.StartDialogue(dialogueTest2);
    }
}
