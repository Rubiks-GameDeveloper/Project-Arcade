using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ForFunOnLevels
{
    public class Ladder : MonoBehaviour
    {
        [SerializeField] private GameObject upDownUI;
        [SerializeField] private bool isPlayerLowerPosition;
        public GameObject upperExit;
        public GameObject lowerExit;
        

        public void PlayerLadderTeleportation(GameObject player)
        {
            if (isPlayerLowerPosition)
            {
                player.transform.position = upperExit.transform.position;
                isPlayerLowerPosition = false;
            }
            else
            {
                player.transform.position = lowerExit.transform.position;
                isPlayerLowerPosition = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                upDownUI = other.GetComponent<PlayerProgrammingTransformer>().ladderButton;
                upDownUI.SetActive(true);
                other.GetComponent<PlayerProgrammingTransformer>().Ladder = gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                upDownUI.SetActive(false);
                other.GetComponent<PlayerProgrammingTransformer>().Ladder = gameObject;
            }
        }
    }
}
