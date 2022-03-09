using System;
using System.Collections;
using FightSystem;
using UnityEngine;

namespace TrapsScripts
{
    public class ArrowTrap : MonoBehaviour
    {
        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                rb.isKinematic = false;
            }
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            StartCoroutine(CreatureDie(col.gameObject));
        }
        

        
        private IEnumerator CreatureDie(GameObject creature)
        {
            if (creature.CompareTag("Player"))
            {
                var player = creature.GetComponent<ProgrammingPlayerFightSystem>();
                while (player.currentPlayerHealth > 0)
                {
                    player.PlayerDamageTaking(2);
                    yield return new WaitForSeconds(1f);
                }
            }
            Destroy(gameObject,.3f);
        }
    }
}
