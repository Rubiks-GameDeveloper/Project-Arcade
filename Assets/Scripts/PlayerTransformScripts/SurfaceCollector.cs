using System;
using UnityEditor;
using UnityEngine;

public class SurfaceCollector : MonoBehaviour
{
    private Vector3 _normal;

    public Vector3 Projection(Vector3 direction)
    {
        return direction - Vector3.Dot(direction, _normal) * _normal;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].collider.gameObject.CompareTag("Ground")) _normal = collision.contacts[0].normal;
        else
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                if (collision.contacts[i].collider.gameObject.CompareTag("Ground"))
                {
                    _normal = collision.contacts[i].normal;
                    break;
                }
            }
        }
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        Debug.DrawRay(pos, Projection(transform.right), Color.blue);
        Debug.DrawRay(pos, _normal, Color.cyan);
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        Gizmos.DrawLine(pos, pos + _normal * 3);
    }
}
