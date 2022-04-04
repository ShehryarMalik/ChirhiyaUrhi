using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct words
{
    public string Name;
    public AudioSource sound;
    public bool canFly;
}

[System.Serializable]
public class Vocab
{
    public words[] wordStruct;
}
