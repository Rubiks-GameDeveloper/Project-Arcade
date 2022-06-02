using System;
using UnityEditor;
using UnityEngine;

public class SurfaceCollector : MonoBehaviour
{
    private Vector3 _normal;
    private Collider2D _playerCollider;

    private void Start()
    {
        _playerCollider = gameObject.GetComponent<Collider2D>();
    }

    public Vector3 Projection(Vector3 direction)
    {
        return direction - Vector3.Dot(direction, _normal) * _normal;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var parentPos = gameObject.transform.position;
        for (int i = 0; i < collision.contactCount; i++)
        {
            if (collision.contacts[i].collider.gameObject.CompareTag("Ground") 
                && collision.contacts[i].point.y < parentPos.y + 0.2f 
                && collision.contacts[i].point.x < gameObject.transform.position.x + 0.401f 
                && collision.contacts[i].point.x > gameObject.transform.position.x - 0.401f)
            {
                if (collision.contacts[i].normal.y > 0) _normal = collision.contacts[i].normal;
                else _normal = new Vector3(collision.contacts[i].normal.x, collision.contacts[i].normal.y * -1, 0);
                break;
            }
        }
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        Debug.DrawRay(pos, Projection(transform.right), Color.blue);
        Debug.DrawRay(pos, _normal, Color.red);
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        //Gizmos.DrawLine(pos, pos + _normal * 3);
    }
}
