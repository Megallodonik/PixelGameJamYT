using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestorer : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private Player_Health player_Health;
    private bool _restoreHealth = false;
    public int RestorationValue = 1;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        player_Health = Player.GetComponent<Player_Health>();
        
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
