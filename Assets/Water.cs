using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float waterDensity = 1.0f; // ��������� ����
    public float buoyancyForce = 5.0f; // ���� ��������� ����������
    public float waterDrag = 0.1f; // ������������� ����
    public float waterSurfaceHeight = 0.5f; // ������ ������ ����������� ������������ ������ �������

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
