using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ladder : MonoBehaviour
{
    [SerializeField] private Joystick playerJoystick;

    float speed = 5;

    void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Rigidbody2D>().gravityScale = 0;

        if (other.gameObject.CompareTag("Player"))
        {
            if (playerJoystick.Vertical >= 0 )
            {
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            }
            else if (playerJoystick.Vertical < 0)
            {
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            }
            else
            {
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                other.GetComponent<Rigidbody2D>().gravityScale = 1;
                
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
