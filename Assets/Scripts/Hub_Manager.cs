using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hub_Manager : MonoBehaviour
{
    [SerializeField] GameObject HubUI;
    [SerializeField] TMP_Text OrePriceText;
    [SerializeField] TMP_Text MushroomPriceText;
    [SerializeField] TMP_Text DescriptionText;
    [SerializeField] GameObject HealthRestoration;
    private Player_Harvesting _playerHarvesting;

    private int currentPriceNode = 0;

    public PricesNode[] Prices;

    void Start()
    {
        OrePriceText.text = Prices[currentPriceNode].priceOre.ToString();
        MushroomPriceText.text = Prices[currentPriceNode].priceMushroom.ToString();
        DescriptionText.text = Prices[currentPriceNode].UpgradeDescription;

        _playerHarvesting = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Harvesting>();

        Debug.Log(currentPriceNode);
        Debug.Log(Prices[currentPriceNode].priceOre);

    }



    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpgradeSwitch()
    {
        if (_playerHarvesting._mushroomCount >= Prices[currentPriceNode].priceMushroom && _playerHarvesting._oreCount >= Prices[currentPriceNode].priceOre)
        {
            UpgradeActivation(Prices[currentPriceNode].UpgradeName);
            _playerHarvesting._mushroomCount -= Prices[currentPriceNode].priceMushroom;
            _playerHarvesting._oreCount -= Prices[currentPriceNode].priceOre;


            currentPriceNode++;
            DescriptionText.text = Prices[currentPriceNode].UpgradeDescription;
            OrePriceText.text = Prices[currentPriceNode].priceOre.ToString();
            MushroomPriceText.text = Prices[currentPriceNode].priceMushroom.ToString();
        }

    }

    private void UpgradeActivation(PricesNode.Upgrades upgrade)
    {
        switch (upgrade)
        {
            case PricesNode.Upgrades.HealthRestoration:
                _healthRestorationUpgrade();
                break; 
        }

    }

    private void _healthRestorationUpgrade()
    {
        HealthRestoration.SetActive(true);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HubUI.SetActive(true);
            _playerHarvesting.HarvestableText(Player_Harvesting.TextActions.Appear, Player_Harvesting.Harvestables.Both, 0);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("Player"))
        {
            UpgradeSwitch();

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HubUI.SetActive(false);
            _playerHarvesting.HarvestableText(Player_Harvesting.TextActions.Disappear, Player_Harvesting.Harvestables.Both, 0);
        }
    }
}

[System.Serializable]
public class PricesNode 
{
    public int priceOre;
    public int priceMushroom;
    public Upgrades UpgradeName;
    public string UpgradeDescription;
    public enum Upgrades
    {
        HealthRestoration = 0,
        SomeSecondUpgrade = 1,
        SomeThirdUpgrade = 2,
    }
}