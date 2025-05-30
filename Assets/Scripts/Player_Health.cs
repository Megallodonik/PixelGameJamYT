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


    public float DamageGlowSpeed = 1f;

    private TMP_Text healthBarText;
    void Start()
    {
        light = LightObj.GetComponent<Light2D>();
        lightMinRadius = light.pointLightOuterRadius;
        lightColor = light.color;



        healthBarText = HealthBar.GetComponent<TMP_Text>();
    }

    public async UniTask PlayerHpChange(int damage)
    {
        HealthBar.SetActive(true);
        PlayerHealth -= damage;
        if (!glow & !unGlow)
        {
            glowOnDamage();
        }
        
        if (PlayerHealth <= 0)
        {
            Death();
        }
        await UniTask.Delay(3000); //3sec
        HealthBar.SetActive(false);
    }

    private async UniTask glowOnDamage()
    {
        glow = true;
        light.color = Color.red;

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
