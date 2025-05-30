using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Harvestable : MonoBehaviour
{
    [SerializeField] private GameObject _harvestText;
    [SerializeField] protected GameObject Glow;

    public float GlowingSpeed = 0.1f;
    public float minGlow = 1f;
    public float maxGlow = 2f;


    protected float glowIntensity;

    private void Start()
    {
        glowIntensity = Glow.GetComponent<Light2D>().intensity;
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
        glowIntensity = math.lerp(1f, 5f, GlowingSpeed * Time.deltaTime);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _harvestText.SetActive(false);
        }
    }
}
