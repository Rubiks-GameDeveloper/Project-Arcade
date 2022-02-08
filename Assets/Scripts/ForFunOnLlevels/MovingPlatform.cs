using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private float dirX;
    private float speed = 3f;
    private bool movingRight = true;
    
    void Update()
    {
        if (transform.position.x > 141f)
        {
            movingRight = false;
        }
        else if (transform.position.x < 127f)
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
}
