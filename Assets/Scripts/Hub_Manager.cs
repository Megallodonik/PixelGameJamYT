using Cysharp.Threading.Tasks;
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

    [SerializeField] GameObject SecondScreenWall;
    [SerializeField] GameObject ThirdScreenWall;
    [SerializeField] CameraController Camera;
    [SerializeField] GameObject SecondScreenArrow;
    [SerializeField] GameObject ThirdScreenArrow;
    private Player_Harvesting _playerHarvesting;
    private bool canUpgrade = true;

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
            canUpgrade = false;
            UpgradeDelay();
            UpgradeActivation(Prices[currentPriceNode].UpgradeName);
            _playerHarvesting._mushroomCount -= Prices[currentPriceNode].priceMushroom;
            _playerHarvesting._oreCount -= Prices[currentPriceNode].priceOre;


            currentPriceNode++;
            DescriptionText.text = Prices[currentPriceNode].UpgradeDescription;
            OrePriceText.text = Prices[currentPriceNode].priceOre.ToString();
            MushroomPriceText.text = Prices[currentPriceNode].priceMushroom.ToString();
        }
        
    }
    private async UniTask UpgradeDelay()
    {
        await UniTask.Delay(1000);
        canUpgrade = true;
    }
    private void UpgradeActivation(PricesNode.Upgrades upgrade)
    {
        switch (upgrade)
        {
            case PricesNode.Upgrades.HealthRestoration:
                _healthRestorationUpgrade();
                break;
            case PricesNode.Upgrades.SecondScreen:
                _secondScreenUpgrade();
                break;
            case PricesNode.Upgrades.ThirdScreen:
                _ThirdScreenUpgrade();
                break;
        }

    }
    private async UniTask _ThirdScreenUpgrade()
    {

        Camera.SecondScreenCameraMove(2);
        await UniTask.Delay(3000);
        ThirdScreenWall.SetActive(false);
        ThirdScreenArrow.SetActive(true);
    }
    private async UniTask _secondScreenUpgrade()
    {
        
        Camera.SecondScreenCameraMove(1);
        await UniTask.Delay(2000);
        SecondScreenWall.SetActive(false);
        SecondScreenArrow.SetActive(true);
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
        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("Player") && canUpgrade)
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
        SecondScreen = 0,
        HealthRestoration = 1,
        ThirdScreen = 2,
    }
}