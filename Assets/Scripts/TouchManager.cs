using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    bool isTouchDown = false;

    public void setTouchDown()
    {
        isTouchDown = true;
        //Debug.Log("Is Touching");
    }
    public void setTouchUP()
    {
        isTouchDown = false;
        //Debug.Log("Stopped Touching");
    }

    public bool getTouchState()
    {
        return isTouchDown;
    }
}
