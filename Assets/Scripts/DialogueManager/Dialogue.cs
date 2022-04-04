using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct sentences
{
    public Sprite avatarImage;
    public string speakerName;
    [TextArea(3, 10)]
    public string sentence;
    public AudioSource TypeNoteSFX;
}

[System.Serializable]
public class Dialogue
{
    public sentences[] sentenceStruct;
    public bool dialogueStarted, dialogueFinished;
}
