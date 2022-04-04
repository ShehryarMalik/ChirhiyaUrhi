using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] TouchManager touchManager;

    [SerializeField] bool isReady = false;
    [SerializeField] bool PlayerStateDown = false;

    [SerializeField] bool checkForStateChange = false;
    [SerializeField] bool stateChangeTrigger = false;

    [SerializeField] bool fingerDownFXPlayed = false;
    [SerializeField] ParticleSystem placeFingerFX;
    [SerializeField] ParticleSystem fingerSpark1FX, fingerSpark2FX;
    [SerializeField] AudioSource fingerDownSFX;

    public bool enableSparks = false;

    private void Start()
    {
        //anim.GetComponent<Animator>();
    }

    private void Update()
    {
        updateTouchState();
        handleAnimations();

        //if(stateChangeTrigger)
    }
        
    //public void playFingerDownFX()
    //{
    //    placeFingerFX.Play(true);
    //}

    public void StopSparks()
    {
        fingerSpark1FX.Stop();
        fingerSpark2FX.Stop();
    }

    void PlaceFingerDownFX()
    {
        if(!fingerDownFXPlayed && !placeFingerFX.isPlaying)
        {
            fingerDownFXPlayed = true;
            placeFingerFX.Play(true);
            fingerDownSFX.Play();

            if(enableSparks)
            {
                fingerSpark1FX.Play();
                fingerSpark2FX.Play();
            }

        }
    }

    void updateTouchState()
    {
        //change trigger
        if(PlayerStateDown != touchManager.getTouchState())
        {
            StartCoroutine("touchChangeTrigger");
        }

    }

    IEnumerator touchChangeTrigger()
    {
        yield return new WaitForSeconds(0.1f);
        if (PlayerStateDown != touchManager.getTouchState())
        {
            PlayerStateDown = touchManager.getTouchState();
        }
        
        //while(PlayerStateDown != touchManager.getTouchState())
        //{
        //    yield return null;
        //}
    }

    public bool CheckMove(bool x)
    {
        x = PlayerStateDown;

        return x;
    }

    IEnumerator checkFlyMove()
    {
        yield return new WaitForSeconds(1.5f);
    }
    void handleAnimations()
    {
        //If match is started
        if (isReady)        
        {
            anim.SetBool("StateUp", true);

            //If Player is touching the screen
            if (PlayerStateDown)
            {
                PlaceFingerDownFX();
                anim.SetBool("StateDown", true);
            }

            //If player is not touching the screen
            else
            {
                fingerDownFXPlayed = false;

                if(enableSparks)
                {
                    fingerSpark1FX.Stop();
                    fingerSpark2FX.Stop();
                }

                anim.SetBool("StateDown", false);
            }
        }

        //If match is not started
        else
        {
            anim.SetBool("StateUp", false);
            anim.SetBool("StateDown", false);
        }
    }

    public bool getPlayerStateDown()
    {
        return PlayerStateDown;
    }

    public void setPlayerStateReady(bool s)
    {
        if (isReady != s)
        {
            isReady = s;
        }
    }

    public void ToggleReady()
    {
        isReady = !isReady;
    }

    public void SetReady()
    {
        isReady = true;
    }

    public void UnsetReady()
    {
        isReady = false;
    }
}
