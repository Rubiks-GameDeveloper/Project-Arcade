using System;
using UnityEngine;

public class VectorMovement : MonoBehaviour
{
    [SerializeField] private SurfaceCollector surfaceCollector;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float speed;

    public void Move(Vector3 direction)
    {
        Vector3 directionAlongSurface = surfaceCollector.Projection(direction.normalized);
        Vector3 offset = directionAlongSurface * (speed * Time.deltaTime);
        
        
        gameObject.transform.position += offset;
    }
}
