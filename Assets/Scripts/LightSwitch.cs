using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] List<GameObject> FloodLight_List;
    [SerializeField] GameObject EnableText;
    [SerializeField] Color OffColor;
    private Light2D _light;
    private Color OnColor;
    public List<Light2D> LightComponent_List;

    private bool canActivate = true;
    void Start()
    {
        for (int i = 0; i < FloodLight_List.Count; i++)
        {
            LightComponent_List.Add(FloodLight_List[i].GetComponent<Light2D>());

        }
        _light = this.GetComponent<Light2D>();
        OnColor = _light.color;
    }

    public async UniTask TurnOnLight()
    {
        Debug.Log("switchLight");
        Debug.Log(canActivate);
        canActivate = false;
        EnableText.SetActive(false);
        _light.color = OffColor;
        for (int i = 0; i < LightComponent_List.Count; i++)
        {
            LightComponent_List[i].enabled = !LightComponent_List[i].enabled;
            await UniTask.Delay(1000);
        }
        await UniTask.Delay(6000);
        TurnOfLight();
        
    }
    public async UniTask TurnOfLight()
    {

        for (int i = 0; i < LightComponent_List.Count; i++)
        {
            LightComponent_List[i].enabled = !LightComponent_List[i].enabled;
            await UniTask.Delay(1000);
        }
        canActivate = true;
        _light.color = OnColor;
    }

    void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canActivate)
        {
            EnableText.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EnableText.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (Input.GetKeyDown(KeyCode.E) && canActivate)
            {
                TurnOnLight();
            }
        }

    }

}
