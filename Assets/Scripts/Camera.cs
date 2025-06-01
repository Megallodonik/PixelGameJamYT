using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;
    private int ScreenLength = 20;
    //public cameraPositionsNode[] CameraPositions;
    //private int currentCameraPosition = 0;
    // Update is called once per frame

    private void Start()
    {

    }
    void FixedUpdate()
    {
        //if (_playerPosition.position.x > CameraPositions[currentCameraPosition].CameraPosition.position.x + ScreenLength / 2)
        //{
        //    currentCameraPosition++;
        //    transform.position = CameraPositions[currentCameraPosition].CameraPosition.position;

        //}
        //if (_playerPosition.position.x < CameraPositions[currentCameraPosition].CameraPosition.position.x - ScreenLength / 2)
        //{
        //    currentCameraPosition -= 1;
        //    transform.position = CameraPositions[currentCameraPosition].CameraPosition.position;

        //}

        if (_playerPosition.position.x > transform.position.x + ScreenLength / 2)
        {
            transform.position = new Vector3(transform.position.x + ScreenLength, transform.position.y, transform.position.z);

        }
        if (_playerPosition.position.x < transform.position.x - ScreenLength / 2)
        {
            transform.position = new Vector3(transform.position.x - ScreenLength, transform.position.y, transform.position.z);

        }


    }
}

//[System.Serializable]
//public class cameraPositionsNode
//{
//    public Transform CameraPosition;
    
//}