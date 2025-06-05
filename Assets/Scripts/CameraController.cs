using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _playerPosition;
    [SerializeField] Vector3 SecondScreenCameraPosition;
    [SerializeField] Vector3 ThirdScreenCameraPosition;
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

    public void CameraMove(int upgrade)
    {
        if (upgrade == 1)
        {
            CameraMove_firstUpgrade();
        }
        else
        {
            CameraMove_secondUpgrade();
        }
    }
    public async UniTask CameraMove_firstUpgrade()
    {
        Debug.Log(delayEnd);
        
        transform.position = Vector3.Lerp(transform.position, SecondScreenCameraPosition, CameraSpeed * Time.deltaTime);

        await UniTask.WaitForFixedUpdate();
        if (!delayEnd)
        {
            CameraMove_firstUpgrade();
        }
        

        
    }
    public async UniTask CameraMove_secondUpgrade()
    {
        Debug.Log(delayEnd);

        transform.position = Vector3.Lerp(transform.position, ThirdScreenCameraPosition, CameraSpeed * Time.deltaTime);

        await UniTask.WaitForFixedUpdate();
        if (!delayEnd)
        {
            CameraMove_secondUpgrade();
        }



    }

    public async UniTask SecondScreenCameraMove(int upgrade)
    {
        Debug.Log("secondscreencameramove");
        delayEnd = false;
        isCameraMoving = true;
        CameraMove(upgrade);
        

        await UniTask.Delay(5000);

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