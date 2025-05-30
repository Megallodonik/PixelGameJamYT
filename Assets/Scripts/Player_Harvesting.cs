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
    [SerializeField] GameObject Mushroom_count;
    [SerializeField] GameObject Ore_count;

    [SerializeField] GameObject Mushroom_icon;
    [SerializeField] GameObject Ore_icon;


    private TMP_Text Mushroom_countText;
    private TMP_Text Ore_countText;
    private int _mushroomCount = 0;
    private int _oreCount = 0;

    private bool _canHarvest = false;

    private CancellationToken _cts_harvesting; // токен для отмены задержки перед сбором ресурса, если игрок отпустил клавишу или вышел из коллайдера
    
    private GameObject _currentHarvestableObject = null;


    private CancellationTokenSource _cts;

    bool delayEnd = false;
    void Start()
    {
        Mushroom_countText = Mushroom_count.GetComponent<TMP_Text>();
        Ore_countText = Ore_count.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Mushroom_countText.text = _mushroomCount.ToString();
        Ore_countText.text = _oreCount.ToString();

        if (Input.GetKeyDown(KeyCode.E) & _canHarvest)
        {
            
            Debug.Log("E is pressed");
            
            harvestDelay();
            Harvesting();



        }

    }


    private async UniTask harvestDelay()
    {

        _cts = new CancellationTokenSource();

        delayEnd = false;

        
        try
        {
            Debug.Log("try delaay");
            await UniTask.Delay(3000, cancellationToken: _cts.Token); //3 sec
            Debug.Log("delayend");
            delayEnd = true;


        }
        catch (OperationCanceledException)
        {
            Debug.Log("cancelled");
            delayEnd = false;
        }

    }

    private async UniTask Harvesting()
    {

        
        Debug.Log("Harvesting");
        
        _currentHarvestableObject.GetComponent<Harvestable>().Glowing();


        while (_canHarvest)
        {
            Debug.Log(delayEnd);
            GameObject harvestableObj = _currentHarvestableObject;
            Debug.Log("while _canHarvest");

            
            if (_canHarvest & harvestableObj == _currentHarvestableObject & delayEnd == true)
            {
                
                switch (harvestableObj.tag)
                {
                    case "Mushroom":
                        Debug.Log("Mushroom");
                        textDisapearing(harvestableObj.tag);
                         _mushroomCount++;

                        harvestableObj.SetActive(false);

                        break;
                    case "Ore":
                        Debug.Log("Ore");
                        textDisapearing(harvestableObj.tag);
                        _oreCount++;
                        harvestableObj.SetActive(false);
                        break;
                }

                Debug.Log("end of switchcase");
            }
            

            await UniTask.Delay(500);

        }
        
        

    }

    private async UniTask textDisapearing(string harvestable)
    {
        switch (harvestable)
        {
            case "Mushroom":
                Debug.Log("Mushroom");
                Mushroom_count.SetActive(true);
                Mushroom_icon.SetActive(true);

                await UniTask.Delay(3000);

                Mushroom_icon.SetActive(false);
                Mushroom_count.SetActive(false);
                break;
            case "Ore":
                Debug.Log("Ore");
                Ore_count.SetActive(true);
                Ore_icon.SetActive(true);

                await UniTask.Delay(3000);

                Ore_icon.SetActive(false);
                Ore_count.SetActive(false);

                break;
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
            _currentHarvestableObject = null;
            _canHarvest = true;
            _currentHarvestableObject = other.gameObject;


        }
    }

    public void StopHarvestDelay()
    {
        _cts.Cancel(); 
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Mushroom") || other.CompareTag("Ore"))
        {
            Debug.Log("is not harvestable");

            _canHarvest = false;
            _currentHarvestableObject.GetComponent<Harvestable>().stopGlowing();
            StopHarvestDelay();



        }
    }

}
