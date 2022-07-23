using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ForFunOnLevels
{
    public class Ladder : MonoBehaviour
    {
        [SerializeField] private float movingUpSpeed;
        [SerializeField] private GameObject upDownUI;
        public UnityEvent playerInLadder;

        private void Start()
        {
            //playerInLadder.AddListener(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerProgrammingTransformer>());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerInLadder.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerInLadder.Invoke();
            }
        }
    }
}
