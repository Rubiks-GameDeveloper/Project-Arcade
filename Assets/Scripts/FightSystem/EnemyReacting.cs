using System;
using UnityEngine;

namespace FightSystem
{
    public class EnemyReacting : MonoBehaviour
    {
        [SerializeField] private Enemy data;
        public bool isPlayerInVision;
        public GameObject player;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            //if (other.CompareTag("Player")) data.EnemyAngryStateActivate();
            if (!other.CompareTag("Player")) return;
            isPlayerInVision = true; player = other.gameObject;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player")) isPlayerInVision = false;
            player = null;
        }
    }
}
