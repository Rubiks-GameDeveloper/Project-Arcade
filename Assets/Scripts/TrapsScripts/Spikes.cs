using System;
using System.Collections;
using FightSystem;
using UnityEngine;

namespace TrapsScripts
{
    public class Spikes : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            StartCoroutine(CreatureDie(col.gameObject));
        }

        private void OnCollisionExit2D(Collision2D col)
        {
            StopAllCoroutines();
        }

        private IEnumerator CreatureDie(GameObject creature)
        {
            if (creature.CompareTag("Player"))
            {
                var player = creature.GetComponent<ProgrammingPlayerFightSystem>();
                while (player.currentPlayerHealth > 0)
                {
                    player.PlayerDamageTaking(2);
                    yield return new WaitForSeconds(0.35f);
                }
            }
            else
            {
                var enemy = creature.GetComponent<Enemy>();
                while (enemy.enemyHealth > 0)
                {
                    //StartCoroutine(enemy.DamageTaking(2));
                    yield return new WaitForSeconds(0.35f);
                }
            }
        }
    }
}
