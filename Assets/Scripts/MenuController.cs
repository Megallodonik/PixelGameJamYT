using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Tutorial[] tutorial;
    [SerializeField] GameObject TutorialBorder;
    [SerializeField] GameObject StartButton;

    [SerializeField] AudioClip buttonPressed;
    [SerializeField] AudioSource audioSource;
    private int currentTutorialNode = 0;
    public void StartTutorial()
    {
        audioSource.clip = buttonPressed;
        audioSource.Play();
        for (int i = 0; i < tutorial[currentTutorialNode].nodeGameObjects.Count; i++)
        {
            tutorial[currentTutorialNode].nodeGameObjects[i].SetActive(true);
        }
        StartButton.SetActive(false);
        TutorialBorder.SetActive(true);    
    }

    void Start()
    {
        
    }

    public void ChangeNode()
    {
        audioSource.clip = buttonPressed;
        audioSource.Play();
        if (tutorial[currentTutorialNode].StartGame)
        {
            SceneManager.LoadScene("Main");
        }
        else
        {
            for (int i = 0; i < tutorial[currentTutorialNode].nodeGameObjects.Count; i++)
            {
                tutorial[currentTutorialNode].nodeGameObjects[i].SetActive(false);
            }
            currentTutorialNode++;
            for (int i = 0; i < tutorial[currentTutorialNode].nodeGameObjects.Count; i++)
            {
                tutorial[currentTutorialNode].nodeGameObjects[i].SetActive(true);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Tutorial
{
    public List<GameObject> nodeGameObjects;
    public bool StartGame;
}