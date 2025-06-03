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
    public int _mushroomCount = 0;
    public int _oreCount = 0;

    private bool _canHarvest = true;

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

        if (_mushroomCount < 0)
        {
            _mushroomCount = 0;
        }
        if (_oreCount < 0)
        {
            _oreCount = 0;
        }
    }


    private async UniTask harvestDelay()
    {

        _cts = new CancellationTokenSource();

        delayEnd = false;
        _canHarvest = false;

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
            _canHarvest = true;
            delayEnd = false;
        }

    }

    private async UniTask Harvesting()
    {

        
        Debug.Log("Harvesting");
        GameObject harvestableObj = _currentHarvestableObject;
        _currentHarvestableObject.GetComponent<Harvestable>().Glowing();
        await UniTask.WaitUntil(() => delayEnd);
        if (harvestableObj == _currentHarvestableObject)
        {
            delayEnd = false;
            switch (harvestableObj.tag)
            {
                case "Mushroom":
                    Debug.Log("Mushroom");
                    textDisapearing(harvestableObj.tag);
                    _mushroomCount += harvestableObj.GetComponent<Harvestable>().YieldEmount;

                    harvestableObj.SetActive(false);
                    _canHarvest = true;
                    break;
                case "Ore":
                    Debug.Log("Ore");
                    textDisapearing(harvestableObj.tag);
                    _oreCount += harvestableObj.GetComponent<Harvestable>().YieldEmount;
                    harvestableObj.SetActive(false);
                    _canHarvest = true;
                    break;
            }

            Debug.Log("end of switchcase");
        }

    }

    private async UniTask textDisapearing(string harvestable)
    {
        switch (harvestable)
        {
            case "Mushroom":
                Debug.Log("Mushroom");


                HarvestableText(TextActions.Appear, Harvestables.Mushroom, 3000);
                break;
            case "Ore":
                Debug.Log("Ore");
                HarvestableText(TextActions.Appear, Harvestables.Ore, 3000);

                break;
            case "Both":
                Debug.Log("both");
                HarvestableText(TextActions.Appear, Harvestables.Both, 3000);

                break ;
        }

    }
    public enum Harvestables
    {
        Mushroom,
        Ore,
        Both,
    }
    public enum TextActions
    {
        Appear,
        Disappear,
    }
    public async UniTask HarvestableText(TextActions action, Harvestables harvestable, int delay) // if delay == 0 text will not disappear automatically
    {
        switch (harvestable)
        {
            case Harvestables.Ore:
                switch (action)
                {
                    case TextActions.Appear:

                        Ore_count.SetActive(true);
                        Ore_icon.SetActive(true);
                        if (delay != 0)
                        {
                            await UniTask.Delay(delay);
                            HarvestableText(TextActions.Disappear, harvestable, 0);
                        }
                        break;
                    case TextActions.Disappear:
                        Ore_icon.SetActive(false);
                        Ore_count.SetActive(false);

                        break;
                }
                break;
            case Harvestables.Mushroom:
                switch (action)
                {
                    case TextActions.Appear:


                        Mushroom_count.SetActive(true);
                        Mushroom_icon.SetActive(true);
                        if (delay != 0)
                        {
                            await UniTask.Delay(delay);
                            HarvestableText(TextActions.Disappear, harvestable, 0);
                        }
                        break;
                    case TextActions.Disappear:


                        Mushroom_icon.SetActive(false);
                        Mushroom_count.SetActive(false);

                        break;
                }
                break;
            case Harvestables.Both:
                switch (action)
                {
                    case TextActions.Appear:

                        Ore_count.SetActive(true);
                        Ore_icon.SetActive(true);

                        Mushroom_count.SetActive(true);
                        Mushroom_icon.SetActive(true);
                        if (delay != 0)
                        {
                            await UniTask.Delay(delay);
                            HarvestableText(TextActions.Disappear, harvestable, 0);
                        }
                        break;
                    case TextActions.Disappear:
                        Ore_icon.SetActive(false);
                        Ore_count.SetActive(false);

                        Mushroom_icon.SetActive(false);
                        Mushroom_count.SetActive(false);

                        break;
                }
                break;

        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("trigger stay");

        if (Input.GetKeyDown(KeyCode.E) && _canHarvest && (other.CompareTag("Mushroom") || other.CompareTag("Ore")))
        {

            Debug.Log("E is pressed");
            _currentHarvestableObject = null;
            
            _currentHarvestableObject = other.gameObject;
            harvestDelay();
            Harvesting();



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
            StopHarvestDelay();
            _currentHarvestableObject.GetComponent<Harvestable>().stopGlowing();

        }
    }

}
