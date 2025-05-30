using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hub_Manager : MonoBehaviour
{
    [SerializeField] GameObject HubUI;
    [SerializeField] TMP_Text OrePriceText;
    [SerializeField] TMP_Text MushroomPriceText;

    private int currentPriceNode = 0;

    public PricesNode[] Prices;

    void Start()
    {
        OrePriceText.text = Prices[currentPriceNode].priceOre.ToString();
        MushroomPriceText.text = Prices[currentPriceNode].priceMushroom.ToString();

        Debug.Log(currentPriceNode);
        Debug.Log(Prices[currentPriceNode].priceOre);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HubUI.SetActive(true);
            currentPriceNode++;
            OrePriceText.text = Prices[currentPriceNode].priceOre.ToString();
            MushroomPriceText.text = Prices[currentPriceNode].priceMushroom.ToString();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HubUI.SetActive(false);
        }
    }
}

[System.Serializable]
public class PricesNode
{
    public float priceOre;
    public float priceMushroom;


}