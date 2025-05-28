using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    


    public float MovementSpeed = 1f;
    public float JumpForce = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        float translation = Input.GetAxis("Horizontal") * Time.deltaTime * MovementSpeed;
        
        transform.Translate(translation, 0, 0);


    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
           
            Vector2 force = Vector2.up * JumpForce;
            _rb.AddForce(force, ForceMode2D.Impulse);
        }

    }



    

}
