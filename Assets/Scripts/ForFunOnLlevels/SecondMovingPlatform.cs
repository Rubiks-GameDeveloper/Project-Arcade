using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMovingPlatform : MonoBehaviour
{
    private float dirX;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float distance1;
    [SerializeField] private float distance2;
    private bool movingUp = true;
    void Update()
    {
            if (transform.position.y > distance1)
            {
                movingUp = false;
            }
            else if (transform.position.y < distance2)
            {
                movingUp = true;
            }
    
            if (movingUp)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
            }
    }
}
