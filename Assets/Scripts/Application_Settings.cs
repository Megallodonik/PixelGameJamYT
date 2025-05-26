using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Application_Settings : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int _targetFPS;

    void Start()
    {
        Application.targetFrameRate = _targetFPS;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
