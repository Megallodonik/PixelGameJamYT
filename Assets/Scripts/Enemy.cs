using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int damage;

    Player_Health player_Health;

    private bool canDamage = true;

    private async UniTask OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            damagePlayer(other.gameObject);
        }
    }


    private async UniTask damagePlayer(GameObject player)
    {
        if (canDamage)
        {
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
