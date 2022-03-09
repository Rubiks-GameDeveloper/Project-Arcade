using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private float dirX;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float distance1;
    [SerializeField] private float distance2;
    private bool movingRight = true;
    
    void Update()
    {
        if (transform.position.x > distance1)
        {
            movingRight = false;
        }
        else if (transform.position.x < distance2)
        {
            movingRight = true;
        }

        if (movingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }
    
   /* private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
        
    }
    
    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
        }

    }*/
}
