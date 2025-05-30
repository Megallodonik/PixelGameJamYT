using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class Harvestable : MonoBehaviour
{
    [SerializeField] private GameObject _harvestText;
    [SerializeField] protected GameObject Glow;

    public float GlowingSpeed = 0.1f;
    public float minGlowRadius = 0.84f;
    public float maxGlowRadius = 5f;

    private bool glowEnabled = false;

    protected Light2D light;

    private void Start()
    {
        light = Glow.GetComponent<Light2D>();
    }
    private void OnEnable()
    {
        light = Glow.GetComponent<Light2D>();
        light.pointLightOuterRadius = minGlowRadius;
        Glow.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _harvestText.SetActive(true);
        }
    }

    public void Glowing()
    {
        Glow.SetActive(true);
        glowEnabled = true;

    }

   

    private void FixedUpdate()
    {
        if (glowEnabled)
        {
            
            light.pointLightOuterRadius = math.lerp(light.pointLightOuterRadius, maxGlowRadius, GlowingSpeed * Time.deltaTime);
        }
    }

    public void stopGlowing()
    {
        Debug.Log("stopGlowing");
        light.pointLightOuterRadius = minGlowRadius;
        glowEnabled = false;
        Glow.SetActive(false);
    }



    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _harvestText.SetActive(false);
        }
    }
}
