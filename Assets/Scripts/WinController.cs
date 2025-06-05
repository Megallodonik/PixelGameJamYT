using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Player;
    [SerializeField] Player_Movement playerMovement;
    [SerializeField] Player_Health playerHealth;

    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text WinTimerText;

    [SerializeField] GameObject WinCanvas;

    [SerializeField] Timer timer;

    [SerializeField] Transform destination;

    public float speed;
    void Start()
    {
        
    }
    public void Win()
    {
        MovePlayer();
        timer.isRunning = false;
        WinCanvas.SetActive(true);
        playerMovement.enabled = false;
        playerHealth.enabled = false;
        WinTimerText.text = timerText.text; 
    }
    private async UniTask MovePlayer()
    {
        Player.transform.position = Vector3.Lerp(Player.transform.position, destination.position, speed * Time.deltaTime);
        await UniTask.WaitForFixedUpdate();
        MovePlayer();
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Win();
        }
    }
}
