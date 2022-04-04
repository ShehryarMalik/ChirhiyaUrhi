using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    public TMP_Text staminaText;
    public Image staminaBar, healthBar, opHealthBar;
    public bool enableStamina = false;
    float stamina, maxStamina = 100;
    float health, maxHealth = 100;
    float opHealth, maxOpHealth = 100;
    float lerpSpeed;

    [SerializeField] ParticleSystem staminaAttackFX;
    [SerializeField] AudioSource staminaAttackSFX;
    public float staminaPerSecond = 2, staminaHitDamage = 25f;
    [SerializeField] PlayerController playerController;

    private void Start()
    {
        stamina = 0;
        health = 100;
        opHealth = 100;
    }

    private void Update()
    {
        if (stamina > maxStamina) stamina = maxStamina;

        lerpSpeed = 3f * Time.deltaTime;

        StaminaBarFiller();
        HealthFiller();
        OPHealthFiller();

        if(enableStamina && playerController.getPlayerStateDown())
        {
            IncreaseStamina(staminaPerSecond * Time.deltaTime);

            if(stamina >= 100)
            {
                useStaminaAttack();
            }
        }
    }

    public void useStaminaAttack()
    {
        if(stamina >= 100)
        {
            if (staminaAttackSFX)
                staminaAttackSFX.Play();

            if (staminaAttackFX)
                staminaAttackFX.Play(true);

            DecreaseOpHealth(staminaHitDamage);
            setStamina(0f);
        }
    }

    public float getPlayerHealth()
    {
        return health;
    }

    void HealthFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
    }

    public void IncreaseHealth(float amount)
    {
        if (health < maxStamina)
        {
            health += amount;
        }
    }

    public void DecreaseHealth(float amount)
    {
        if (health > 0)
        {
            health -= amount;
        }
    }

    public void setHealth(float amount)
    {
        health = amount;
    }

    void StaminaBarFiller()
    {
        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, stamina / maxStamina, lerpSpeed);
    }

    public void IncreaseStamina(float amount)
    {
        if(stamina < maxStamina)
        {
            stamina += amount;
        }
    }
    public void DecreaseStamina(float amount)
    {
        if (stamina > 0)
        {
            stamina -= amount;
        }
    }

    public void setStamina(float amount)
    {
        stamina = amount;
    }


    public float getOpHealth()
    {
        return opHealth;
    }

    void OPHealthFiller()
    {
        opHealthBar.fillAmount = Mathf.Lerp(opHealthBar.fillAmount, opHealth / maxOpHealth, lerpSpeed);
    }

    public void IncreaseOpHealth(float amount)
    {
        if (opHealth < maxOpHealth)
        {
            opHealth += amount;
        }
    }

    public void DecreaseOpHealth(float amount)
    {
        if (opHealth > 0)
        {
            opHealth -= amount;
        }
    }

    public void setOpHealth(float amount)
    {
        opHealth = amount;
    }
}
