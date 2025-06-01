using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int damage;

    Player_Health player_Health;

    private bool canDamage = true;

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("OnTriggerStay");
        Debug.Log(other.gameObject);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("CompareTag");
            damagePlayer(other.gameObject);
        }
    }
    

    private async UniTask damagePlayer(GameObject player)
    {
        Debug.Log("DamageAttempt");
        if (canDamage)
        {
            Debug.Log("Damage");
            player_Health = player.GetComponent<Player_Health>();
            player_Health.PlayerHpChange(damage);
            invulnerabilityFrames();
            
        }

    }

    private async UniTask invulnerabilityFrames()
    {
        canDamage = false;

        await UniTask.Delay(1000); // 0.5sec

        canDamage = true;
    }
}
