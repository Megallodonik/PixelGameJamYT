using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _playerPosition;
    [SerializeField] Vector3 SecondScreenCameraPosition;
    public float CameraSpeed = 1;
    private int ScreenLength = 20;
    private bool delayEnd = true;
    private bool isCameraMoving = false;
    //public cameraPositionsNode[] CameraPositions;
    //private int currentCameraPosition = 0;
    // Update is called once per frame

    private void Start()
    {
        
    }


    public async UniTask CameraMove()
    {
        Debug.Log(delayEnd);
        
        transform.position = Vector3.Lerp(transform.position, SecondScreenCameraPosition, CameraSpeed * Time.deltaTime);

        await UniTask.WaitForFixedUpdate();
        if (!delayEnd)
        {
            CameraMove();
        }
        

        
    }

    public async UniTask SecondScreenCameraMove()
    {
        Debug.Log("secondscreencameramove");
        delayEnd = false;
        isCameraMoving = true;
        CameraMove();
        

        await UniTask.Delay(3000);

        delayEnd = true;
        isCameraMoving = false;
        transform.position = new Vector3(0, 0, -10);
        

        
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
        
        Debug.Log(isCameraMoving);
        if (!isCameraMoving) {
            if ((_playerPosition.position.x > transform.position.x + ScreenLength / 2))
            {
                transform.position = new Vector3(transform.position.x + ScreenLength, transform.position.y, transform.position.z);

            }
            if ((_playerPosition.position.x < transform.position.x - ScreenLength / 2))
            {
                transform.position = new Vector3(transform.position.x - ScreenLength, transform.position.y, transform.position.z);

            }
        }


 
    }
}

//[System.Serializable]
//public class cameraPositionsNode
//{
//    public Transform CameraPosition;
    
//}