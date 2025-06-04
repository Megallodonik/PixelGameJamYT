using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable_Spawn : MonoBehaviour
{
    [SerializeField] Screens[] ScreenNode;
    void Start()
    {
        Spawn();
    }
    public void Spawn()
    {

        for (int nodei = 0; nodei < ScreenNode.Length; nodei++)
        {
           
            for (int harvestablei = 0; harvestablei <= ScreenNode[nodei].HarvestablesAmount; harvestablei++)
            {
                
                int mushroomi = UnityEngine.Random.Range(0, ScreenNode[nodei].Ore_List.Count - 1);
                
                ScreenNode[nodei].Mushroom_List[mushroomi].SetActive(true);
            }
            for (int harvestablei = 0; harvestablei <= ScreenNode[nodei].HarvestablesAmount; harvestablei++)
            {
                int orei = UnityEngine.Random.Range(0, ScreenNode[nodei].Ore_List.Count - 1);
                
                ScreenNode[nodei].Ore_List[orei].SetActive(true);
                
            }
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Screens
{
    public int HarvestablesAmount;
    public List<GameObject> Ore_List;
    public  List<GameObject> Mushroom_List;
}