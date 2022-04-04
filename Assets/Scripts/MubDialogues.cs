using UnityEngine;

public class MubDialogues : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public Dialogue MubEntryDialogue, MubWinDialogue, MubLoseDialogue;
    public Dialogue AKEntryDialogue, AKWinDialogue, AKLoseDialogue;

    public void TriggerAKEntryDialogueDialogue()
    {
        dialogueManager.StartDialogue(AKEntryDialogue);
    }

    public void TriggerAKWinDialogueDialogue()
    {
        dialogueManager.StartDialogue(AKWinDialogue);
    }

    public void TriggerAKLoseDialogueDialogue()
    {
        dialogueManager.StartDialogue(AKLoseDialogue);
    }

    public void TriggerMubEntryDialogue()
    {
        dialogueManager.StartDialogue(MubEntryDialogue);
    }

    public void TriggerMubWinDialogueDialogue()
    {
        dialogueManager.StartDialogue(MubWinDialogue);
    }

    public void TriggerMubLoseDialogueDialogue()
    {
        dialogueManager.StartDialogue(MubLoseDialogue);
    }
}
