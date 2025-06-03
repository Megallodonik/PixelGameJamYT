using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player_Health : MonoBehaviour
{
    public int PlayerHealth = 5;

    [SerializeField] Transform DeathPosition;
    [SerializeField] GameObject HealthBar;
    [SerializeField] GameObject LightObj;
    [SerializeField] GameObject DeathTextObject;
    [SerializeField] Color EndColor;

    private Color StartColor;
    public int ColorChangeSpeed;
    private TMP_Text deathText;
    private Player_Harvesting _playerHarvesting;

    private bool deathTextDelayEnd = false;
    private Light2D light;
    private bool glow = false;
    private bool unGlow = false;
    private float lightMinRadius;
    public float lightMaxRadius = 5f;
    private Color lightColor;

    public int MaxPlayerHealth = 5;

    public float DamageGlowSpeed = 1f;

    private TMP_Text healthBarText;
    void Start()
    {
        deathText = DeathTextObject.GetComponent<TMP_Text>();
        StartColor = deathText.color;
        _playerHarvesting = GetComponent<Player_Harvesting>();
        light = LightObj.GetComponent<Light2D>();
        lightMinRadius = light.pointLightOuterRadius;
        lightColor = light.color;



        healthBarText = HealthBar.GetComponent<TMP_Text>();
    }

    public async UniTask PlayerHpChange(int damage, HP_ChangeTypes type)
    {
        HealthBar.SetActive(true);
        switch (type)
        {
            case HP_ChangeTypes.damage:
                PlayerHealth -= damage;
                break;
            case HP_ChangeTypes.restoration:
                if (PlayerHealth < MaxPlayerHealth)
                {
                    PlayerHealth += damage;
                }
                
                break;
        }
        
        if (!glow & !unGlow)
        {
            glowOnDamage(type);
        }
        
        if (PlayerHealth <= 0)
        {
            Death();
        }
        await UniTask.Delay(3000); //3sec
        HealthBar.SetActive(false);
    }


   public enum HP_ChangeTypes
    {
        damage,
        restoration
    }
    private async UniTask glowOnDamage(HP_ChangeTypes type)
    {
        glow = true;
        Glowing();
        switch (type)
        {
            case HP_ChangeTypes.damage:
                light.color = Color.red;
                break;
            case HP_ChangeTypes.restoration:
                light.color = Color.green;
                break;
        }
        

        await UniTask.Delay(3000); //3sec
        glow = false;
        unGlow = true;
        unGlowing();  
        
        await UniTask.Delay(3000);
        unGlow = false;
         
        light.color = lightColor;
        light.pointLightOuterRadius = lightMinRadius;
       

    }

    private async UniTask unGlowing()
    {
        while (unGlow)
        {
            light.pointLightOuterRadius = Mathf.Lerp(light.pointLightOuterRadius, lightMinRadius, DamageGlowSpeed * Time.deltaTime);
            await UniTask.WaitForFixedUpdate();
        }
        

    }
    private async UniTask Glowing()
    {
        while (glow)
        {
            light.pointLightOuterRadius = Mathf.Lerp(light.pointLightOuterRadius, lightMaxRadius, DamageGlowSpeed * Time.deltaTime);
            await UniTask.WaitForFixedUpdate();
        }
    }
    
    private async UniTask deathTextDelay()
    {
        
        await UniTask.Delay(3000); //3sec
        deathTextDelayEnd = true;
    }
    private async UniTask deathTextDisappearing()
    {
        while (!deathTextDelayEnd)
        {
            deathText.color = Color.Lerp(deathText.color, EndColor, ColorChangeSpeed * Time.deltaTime);
            await UniTask.WaitForFixedUpdate();
            
        }
        DeathTextObject.SetActive(false);
        deathText.color = StartColor;
        deathTextDelayEnd = false;

    }
    public void Death()
    {
        DeathTextObject.SetActive(true);
        deathTextDelay();
        deathTextDisappearing();
        transform.position = DeathPosition.position;
        PlayerHealth = 1;

        
        _playerHarvesting._mushroomCount -= 5;
        _playerHarvesting._oreCount -= 5;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthBarText.text = PlayerHealth.ToString();




        
    }
}
