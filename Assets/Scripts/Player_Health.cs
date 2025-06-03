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

    [SerializeField] GameObject HealthBar;
    [SerializeField] GameObject LightObj;

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

    public async UniTask unGlowing()
    {
        while (unGlow)
        {
            light.pointLightOuterRadius = Mathf.Lerp(light.pointLightOuterRadius, lightMinRadius, DamageGlowSpeed * Time.deltaTime);
            await UniTask.WaitForFixedUpdate();
        }
        

    }

    public void Death()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthBarText.text = PlayerHealth.ToString();



        if (glow)
        {
            light.pointLightOuterRadius = Mathf.Lerp(light.pointLightOuterRadius, lightMaxRadius, DamageGlowSpeed * Time.deltaTime);
        }
        
    }
}
