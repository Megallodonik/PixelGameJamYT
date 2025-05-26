using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;

    // Update is called once per frame
    void Update()
    {
        if (_playerPosition.position.x > 12.6f)
        {
            transform.position = new Vector3(20, 0, -10);
        }
        if (_playerPosition.position.x < 12.6f)
        {
            transform.position = new Vector3(0, 0, -10);
        }
    }
}
