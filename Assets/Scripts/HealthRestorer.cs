using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HealthRestorer : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private Player_Health player_Health;
    private bool _restoreHealth = false;
    public int RestorationValue = 1;
    private bool glow = false;
    private Light2D light;
    private float lightMaxRadius;
    private float lightMinRadius;
    public int GlowSpeed = 1;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        light = GetComponent<Light2D>();
        lightMinRadius = light.pointLightOuterRadius;
        player_Health = Player.GetComponent<Player_Health>();
        glowing();
    }
    private async UniTask glowDelay()
    {
        glow = true;
        await UniTask.Delay(3000);
    }
    private async UniTask glowing()
    {
        while (glow)
        {
            light.pointLightOuterRadius = Mathf.Lerp(light.pointLightOuterRadius, lightMaxRadius, GlowSpeed * Time.deltaTime);
            await UniTask.WaitForFixedUpdate();
        }
        glowDelay();
        unGlowing();
    }
    private async UniTask unGlowing()
    {
        while (glow)
        {
            light.pointLightOuterRadius = Mathf.Lerp(light.pointLightOuterRadius, lightMinRadius, GlowSpeed * Time.deltaTime);
            await UniTask.WaitForFixedUpdate();
        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private async UniTask healthRestore()
    {
        while (_restoreHealth)
        {
            player_Health.PlayerHpChange(RestorationValue, Player_Health.HP_ChangeTypes.restoration);
            await UniTask.Delay(500); // 0.5sec
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _restoreHealth = true;
            healthRestore();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _restoreHealth = false;
            
        }
    }
}
