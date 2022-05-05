using System;
using UnityEngine;

namespace ForFunOnLevels
{
    public class Ladder : MonoBehaviour
    {
        [SerializeField] private float movingUpSpeed;
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Rigidbody2D>().velocity += Vector2.up * Time.fixedDeltaTime * movingUpSpeed;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Rigidbody2D>().velocity -= Vector2.up * movingUpSpeed * Time.fixedDeltaTime;
            }
        }
    }
}
