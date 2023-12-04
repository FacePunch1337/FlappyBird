using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;

    Vector3 offset;
    Vector3 mousePosition;
    public float maxSpeed = 10;
    Vector2 mouseForce;
    Vector3 lastPosition;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (rigidbody2D)
        {
            mouseForce = (mousePosition - lastPosition) / Time.deltaTime;
            mouseForce = Vector2.ClampMagnitude(mouseForce, maxSpeed);
            lastPosition = mousePosition;

        }
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                rigidbody2D = targetObject.transform.gameObject.GetComponent<Rigidbody2D>();
                offset = rigidbody2D.transform.position - mousePosition;
            }
        }
        if (Input.GetMouseButtonUp(0) && rigidbody2D)
        {
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.AddForce(mouseForce, ForceMode2D.Impulse);
            rigidbody2D = null;
        }

       
    }
    void FixedUpdate()
    {
        if (rigidbody2D)
        {
            rigidbody2D.MovePosition(mousePosition + offset);
        }

    }
}
