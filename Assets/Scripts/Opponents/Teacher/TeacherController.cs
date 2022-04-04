using UnityEngine;
using System.Collections;
public class TeacherController : MonoBehaviour
{
    [SerializeField] Animator Tanim;
    [SerializeField] bool teacherStateReady = false;
    [SerializeField] bool teacherStateUp = false;
    [SerializeField] bool teacherStateDown = false;
    
    [SerializeField] public Vocab vocab;

    int currentRandomIndex = -1;

    public void setTeacherStateReady(bool s)
    {
        if (teacherStateReady != s)
        {
            teacherStateReady = s; 
            Tanim.SetBool("Ready", s);
        }
    }

    public void setTeacherStateUp(bool s)
    {
        //if (teacherStateUp != s)
        //{
            teacherStateUp = s;
            Tanim.SetBool("StateUp", s);
        //}
    }
    public void setTeacherStateDown(bool s)
    {
        if (teacherStateDown != s)
        {
            teacherStateUp = s;
            Tanim.SetBool("StateDown", s);
        }
    }

    public void chooseAWord()
    {
        int totalWords =  vocab.wordStruct.Length;
        currentRandomIndex = Random.Range(0, totalWords);
    }

    public void speakWord()
    {
        if(currentRandomIndex != -1 && vocab.wordStruct[currentRandomIndex].sound != null)
            vocab.wordStruct[currentRandomIndex].sound.Play();
    }

    public void raiseHandOnce()
    {
        StartCoroutine("raiseHandMain");
    }

    IEnumerator raiseHandMain()
    {
        setTeacherStateUp(true);
        yield return new WaitForSeconds(1f);
        setTeacherStateUp(false);
    }

    public bool canWordFly()
    {
        return vocab.wordStruct[currentRandomIndex].canFly;
    }
}
