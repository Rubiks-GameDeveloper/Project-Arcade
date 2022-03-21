using System.Collections;
using UnityEngine;
using FightSystem;

namespace TrapsScripts
{
    public class FloorMovingTrapSpikes : MonoBehaviour
    {
        [SerializeField] private float spikesDamage;
        private void OnTriggerEnter2D(Collider2D col)
        {
            StartCoroutine(CreatureDie(col.gameObject));
        }

        private void OnTriggerExit2D(Collider2D other)
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
                    player.PlayerDamageTaking(spikesDamage);
                    yield return new WaitForSeconds(0.35f);
                }
            }
            else if (creature.CompareTag("Enemy"))
            {
                var enemy = creature.GetComponent<Enemy>();
                while (enemy.enemyHealth > 0)
                {
                    enemy.DamageTaking(spikesDamage);
                    yield return new WaitForSeconds(0.35f);
                }
            }
        }
    }
}
