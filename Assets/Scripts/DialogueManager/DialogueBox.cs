using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] float fadeTimes;
    private void OnEnable()
    {
        gameObject.LeanScale(new Vector3(0, 0, 0), 0);
        gameObject.LeanScale(new Vector3(1,1,1) ,fadeTimes);
    }

    public void close()
    {
        gameObject.LeanScale(new Vector3(0, 0, 0), fadeTimes).setOnComplete(disableObject);
    }

    void disableObject()
    {
        gameObject.SetActive(false);
    }
}
