using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform StartPoint;
    [SerializeField] Transform EndPoint;
    private bool moveToEnd = true;
    public float CameraSpeed;
    void Start()
    {
        cameraMovement();
    }
    private async UniTask cameraMovement()
    {
        while (moveToEnd)
        {
            transform.position = Vector3.Lerp(transform.position, EndPoint.position, CameraSpeed * Time.deltaTime);
            if (transform.position.x > EndPoint.position.x - 1)
            {
                moveToEnd = false;

            }
  
            
            await UniTask.WaitForFixedUpdate();
        }
        
        while (!moveToEnd)
        {
            transform.position = Vector3.Lerp(transform.position, StartPoint.position, CameraSpeed * Time.deltaTime);
            if (transform.position.x < StartPoint.position.x + 1)
            {
                moveToEnd = true;

            }

            await UniTask.WaitForFixedUpdate();
        }
        
        cameraMovement();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
