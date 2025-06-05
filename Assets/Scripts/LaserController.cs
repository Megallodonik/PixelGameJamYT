using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] List<GameObject> Lasers;
    public int Delay;
    void Start()
    {
        switchLasers();


    }
    private async UniTask switchLasers()
    {
        for (int i = 0; i < Lasers.Count; i++)
        {
            Lasers[i].SetActive(false);
        }
        await UniTask.Delay(Delay);
        for (int i = 0; i < Lasers.Count; i++)
        {
            Lasers[i].SetActive(true);
        }
        await UniTask.Delay(Delay);
        switchLasers();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
