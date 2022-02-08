using UnityEngine;

namespace FightSystem
{
    public class EnemyReacting : MonoBehaviour
    {
        [SerializeField] private Enemy data;
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !data.isEnemyAngry) StartCoroutine(data.EnemyFightReaction(other.gameObject));
            //print(1);
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
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            //Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius * 1.88f);
        }
    }
}
