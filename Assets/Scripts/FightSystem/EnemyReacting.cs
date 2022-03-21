using System;
using UnityEngine;

namespace FightSystem
{
    public class EnemyReacting : MonoBehaviour
    {
        [SerializeField] private Enemy data;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) data.EnemyAngryStateActivate();
        }

        private void EnemyPerformance(GameObject player)
        {
            //Shorten distance if necessary (unusual shorten include)
            //Attack
            //Increase the distance to safe
            //Wait for next attack
            //Stay in block (if you have the shield)
            //Go back if player shorten distance
            //If player left visible distance go patrol
        }
    }
}
