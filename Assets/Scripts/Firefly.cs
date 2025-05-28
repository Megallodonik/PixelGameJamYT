using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Firefly : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    public float MovementSpeed = 1f;

    Vector3 targetPos;
    void Start()
    {
        float posX = UnityEngine.Random.Range(-11f, 7f);  
        float posY = UnityEngine.Random.Range(-4f, 4f);


        targetPos = new Vector3(posX, posY, 0);

        Move();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        transform.position = Vector3.Lerp(pos, targetPos, 0.25f * Time.deltaTime);
    }

    async UniTask Move()
    {
        

        float posX = UnityEngine.Random.Range(-11f, 7f);
        float posY = UnityEngine.Random.Range(-4f, 4f);


        targetPos = new Vector3(posX, posY, 0);
        

        await UniTask.Delay(UnityEngine.Random.Range(1000, 5000)); // 1sec - 5 sec
        Move();
    }
}
