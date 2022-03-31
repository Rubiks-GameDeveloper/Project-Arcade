using System;
using UnityEngine;

public class VectorMovement : MonoBehaviour
{
    [SerializeField] private SurfaceCollector surfaceCollector;
    [SerializeField] private Rigidbody2D rb2D;

    public void Move(Vector3 direction, float speed)
    {
        Vector3 directionAlongSurface = surfaceCollector.Projection(direction.normalized);
        Vector3 offset = directionAlongSurface * (speed * Time.deltaTime);
        
        gameObject.transform.position += offset;
    }
    public void Move(Vector3 direction, float speed, Vector3 additionalOffset)
    {
        Vector3 directionAlongSurface = surfaceCollector.Projection(direction.normalized);
        Vector3 offset = directionAlongSurface * (speed * Time.deltaTime);
        
        gameObject.transform.position += offset + additionalOffset;
    }
}
