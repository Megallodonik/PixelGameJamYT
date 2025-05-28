using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Player_Harvesting : MonoBehaviour
{
    [SerializeField] TMP_Text Mushroom_countText;
    [SerializeField] TMP_Text Ore_countText;
    private int _mushroomCount = 0;
    private int _oreCount = 0;

    private bool _canHarvest = false;

    private CancellationToken _cts_harvesting; // токен для отмены задержки перед сбором ресурса, если игрок отпустил клавишу или вышел из коллайдера
    
    private GameObject _HarvestableObject = null;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Mushroom_countText.text = _mushroomCount.ToString();
        Ore_countText.text = _oreCount.ToString();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E is pressed");

            Harvesting();



        }

    }




    private async UniTask Harvesting()
    {
        Debug.Log("Harvesting");
        
        if (_canHarvest)
        {
            Debug.Log("Harvested");
            await UniTask.Delay(5000); //5 sec
            switch (_HarvestableObject.tag)
            {
                case "Mushroom":
                    _mushroomCount++;
                    break;
                case "Ore":
                    _oreCount++;
                    break;
            }

            _HarvestableObject.SetActive(false);

            
        }
    }


    public async UniTask OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("trigger stay");


    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger enter");
        if (other.CompareTag("Mushroom") || other.CompareTag("Ore"))
        {

            _canHarvest = true;
            _HarvestableObject = other.gameObject;


        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Mushroom") || other.CompareTag("Ore"))
        {
            Debug.Log("is not harvestable");
            _canHarvest = false;
            _HarvestableObject = null;
           


        }
    }

}
