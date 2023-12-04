using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float waterDensity = 1.0f; // Плотность воды
    public float buoyancyForce = 5.0f; // Сила поддержки плавучести
    public float waterDrag = 0.1f; // Сопротивление воды
    public float waterSurfaceHeight = 0.5f; // Высота водной поверхности относительно центра объекта

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.attachedRigidbody != null)
        {
            float buoyancy = waterDensity * Mathf.Abs(Physics2D.gravity.y) * Mathf.Abs(other.attachedRigidbody.gravityScale);

           
            Vector2 waterDragForce = -other.attachedRigidbody.velocity * waterDrag;

           
            float depthInWater = Mathf.Clamp(transform.position.y - waterSurfaceHeight, 0f, float.MaxValue);
            float normalizedDepth = Mathf.Clamp01(depthInWater / waterSurfaceHeight);

           
            other.attachedRigidbody.AddForce(Vector2.up * buoyancyForce * buoyancy * normalizedDepth + waterDragForce);
        }
    }
}
