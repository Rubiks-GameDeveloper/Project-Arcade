using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            rb.isKinematic = false;
            Debug.Log("ff");
        }
    }
        
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            Debug.Log("fff");
        }
    }
}
