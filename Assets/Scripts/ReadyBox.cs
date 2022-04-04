using UnityEngine;
using TMPro;

public class ReadyBox : MonoBehaviour
{
    [SerializeField] bool startCountDown = false;
    [SerializeField] GameObject readyTextGO;
    [SerializeField] TMP_Text countDownText;
    [SerializeField] PlayerController playerController;

    int currentCount = 3;

    void Update()
    {
        if(startCountDown && playerController.getPlayerStateDown())
        {
            countDownText.text = currentCount.ToString();
        }
    }
}
