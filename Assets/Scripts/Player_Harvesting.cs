using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Harvesting : MonoBehaviour
{

    private int _HarvestableCount = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (other.CompareTag("Harvestable"))
            {
                other.gameObject.SetActive(false);
                _HarvestableCount++;

            }
        }
    }



}
