using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 10f;

    private Rigidbody2D rigidbody2D;
   
   

    private void Start()
    {
            rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2D.AddForce(Vector2.up * jumpForce);
        }
    }
    


}
