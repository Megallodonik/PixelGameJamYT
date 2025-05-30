using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable_Spawn : MonoBehaviour
{
    [SerializeField] List<GameObject> Ore_List;
    [SerializeField] List<GameObject> Mushroom_List;
    void Start()
    {
        Spawn();
    }
    public void Spawn()
    {



        for (int i = 0; i < 5; i++) 
        {
            int oreIndex = UnityEngine.Random.Range(0, Ore_List.Count);
            Ore_List[oreIndex].SetActive(true);

            int mushroomIndex = UnityEngine.Random.Range(0, Mushroom_List.Count);
            Mushroom_List[mushroomIndex].SetActive(true);
        }





    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
