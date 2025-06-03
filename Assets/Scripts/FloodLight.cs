using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodLight : Enemy
{
    private Collider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Text");
    }
    private void OnEnable()
    {
        colliderDelay();
    }
    private void OnDisable()
    {
        collider.enabled = false;
    }
    private async UniTask colliderDelay()
    {
        await UniTask.Delay(800); // 0.8sec
        collider.enabled = true;
    }
}
