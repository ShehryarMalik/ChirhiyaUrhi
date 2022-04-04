using UnityEngine;

public class SandboxDialogues : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public Dialogue teacherEntryDialogue, matchWinDialogue, matchLoseDialogue;

    public void TriggerTeacherEntryDialogue()
    {
        dialogueManager.StartDialogue(teacherEntryDialogue);
    }

    public void TriggerMatchWinDialogue()
    {
        dialogueManager.StartDialogue(matchWinDialogue);
    }

    public void TriggerMatchLoseDialogue()
    {
        dialogueManager.StartDialogue(matchLoseDialogue);
    }
}
