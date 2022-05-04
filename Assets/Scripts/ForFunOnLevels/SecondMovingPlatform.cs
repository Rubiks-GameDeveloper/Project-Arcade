using System;
using UnityEngine;

namespace ForFunOnLevels
{
    public class SecondMovingPlatform : MonoBehaviour
    {
        [SerializeField] private float speed = 3f;
        [SerializeField] private float distance1;
        [SerializeField] private float distance2;
        
        private bool _movingUp = true;
        private void FixedUpdate()
        {
            if (transform.position.y > distance1) _movingUp = false;
            else if (transform.position.y < distance2) _movingUp = true;

            transform.position = _movingUp ? new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime) : new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            //if (collision.gameObject.CompareTag("Player")) 
        }
        
        
    }
}
