using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player_Health player_Health = other.gameObject.GetComponent<Player_Health>();
            player_Health.PlayerHpChange(damage);
        }
    }

}
