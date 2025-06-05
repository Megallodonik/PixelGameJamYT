using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public float currentTime = 0f;
    public bool isRunning = true;
    [SerializeField] TMP_Text TimerText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            currentTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        }

        
    }

}
