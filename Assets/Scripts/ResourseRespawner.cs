using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourseRespawner : MonoBehaviour
{
    [SerializeField] GameObject UI;
    [SerializeField] GameObject Player;
    [SerializeField] Harvestable_Spawn Spawner;

    
    private Player_Health player_Health;
    void Start()
    {
        player_Health = Player.GetComponent<Player_Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void respawnResourses()
    {
        if (player_Health.PlayerHealth >= 3)
        {
            player_Health.PlayerHpChange(3, Player_Health.HP_ChangeTypes.damage);
            Spawner.Spawn();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UI.SetActive(true);
            player_Health.HealthBar.SetActive(true);
            
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("Player"))
        {
            respawnResourses();

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player_Health.HealthBar.SetActive(false);
            UI.SetActive(false);
            
        }
    }
}
