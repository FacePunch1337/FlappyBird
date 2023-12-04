using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float speed = 4f; 

    private float startPosX; 
    private float length;     

    void Start()
    {
        startPosX = transform.position.x;  
        length = GetComponent<SpriteRenderer>().bounds.size.x;  
    }

    void Update()
    {
        
        float newPos = Mathf.Repeat(Time.time * speed, length);
        transform.position = new Vector2(startPosX - newPos, transform.position.y);
    }
}
