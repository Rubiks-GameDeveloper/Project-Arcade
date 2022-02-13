using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FightSystem
{
    public class Enemy : MonoBehaviour
    {
    
        public int enemyHealth;
        public int enemyArmor;
        private float _nextAttackTime;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float enemyDamage;
        [SerializeField] private float attackTime;
    
        [Header("Values for patrol")]
        [SerializeField] private Transform patrolPoint;
        [SerializeField] private float patrolRange;
        [SerializeField] [Range(0.01f, 1.5f)] private float patrolSpeed;
    
        [Header("Values for reacting on events")]
        [SerializeField] [Range(0.01f, 0.5f)] private float enemyFollowingSpeed;
        [SerializeField] private float attackRange;
        [SerializeField] private float pushingForce;
        [SerializeField] private Transform interactionPoint;
        [SerializeField] private float stunTime;

        private Animator _animator; 
    
        public bool isEnemyAngry;
        private bool _isEnemyStun;

        private bool _isEnemyInAccessible;

        private bool _isEnemyAttack;

        private IEnumerator _enemyDataCoroutine;
        private void Awake()
        {
            isEnemyAngry = false;
            _animator = GetComponent<Animator>();
            GlobalEventManager.enemyAlarm.AddListener(ReactionToAlarm);
            StartPatrol();
            StartCoroutine(GlobalCoroutine());
        }

        private float playerDistanceToEnemy(Transform enemy, Transform player)
        {
            Vector2 distance = player.position - enemy.position;
            return (float)Math.Sqrt(Math.Pow(distance.x, 2) + Math.Pow(distance.y, 2));
        }
        private float playerDistanceToEnemy(Vector3 enemy, Vector3 player)
        {
            Vector2 distance = player - enemy;
            return (float)Math.Sqrt(Math.Pow(distance.x, 2) + Math.Pow(distance.y, 2));
        }
        public void DamageTaking(float damage, Animator animator)
        {
            enemyHealth += (int)(enemyArmor * 0.3f);
            enemyHealth -= (int)damage;
            animator.SetTrigger("DamageTaking");
            EnemyDie(animator.gameObject, animator);
            StartCoroutine(EnemyStunning());
        }
        
        public IEnumerator DamageTaking(float damage)
        {
            _animator.SetTrigger("Hurt");
            if (!_isEnemyInAccessible && transform.rotation.y == 0)
            {
                StopCoroutine(_enemyDataCoroutine);
                if (_enemyDataCoroutine == EnemyPatrolLeft()) _enemyDataCoroutine = EnemyPatrolRight();
                else if (_enemyDataCoroutine == EnemyPatrolRight()) _enemyDataCoroutine = EnemyPatrolLeft();
                transform.rotation = Quaternion.AngleAxis(180, Vector3.down);
            }
            else if (!_isEnemyInAccessible && transform.rotation.y == -1)
            {
                StopCoroutine(_enemyDataCoroutine);
                if (_enemyDataCoroutine == EnemyPatrolLeft()) _enemyDataCoroutine = EnemyPatrolRight();
                else if (_enemyDataCoroutine == EnemyPatrolRight()) _enemyDataCoroutine = EnemyPatrolLeft();
                transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            }
            StartCoroutine(EnemyColorChanging());
            enemyHealth -= (int)damage;
            StartCoroutine(EnemyDie(gameObject));
            yield return null;
            //StopAllCoroutines();
        }

        private void StartPatrol()
        {
            var number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                {
                    CoroutineMemoryStart(EnemyPatrolLeft());
                    break;
                }
                case 1:
                {
                    CoroutineMemoryStart(EnemyPatrolRight());
                    break;
                }
            }
        }
        private IEnumerator EnemyStunning()
        {
            _isEnemyStun = true;
            yield return new WaitForSeconds(stunTime);
            _isEnemyStun = false;
        }

        private void Damage()
        {
            if (Time.time >= _nextAttackTime)
            {
                Collider2D[] player = new Collider2D[20];
                Physics2D.OverlapCircleNonAlloc(transform.position, attackRange + 2, player);
                foreach (Collider2D dataEnemy in player)
                {
                    if (dataEnemy != null && dataEnemy.CompareTag("Player"))
                    {
                        if (!dataEnemy.GetComponent<ProgrammingPlayerFightSystem>().isPlayerBlock)
                        {
                            dataEnemy.GetComponent<ProgrammingPlayerFightSystem>().PlayerDamageTaking(enemyDamage);
                        }
                    }
                }
                _nextAttackTime = Time.time + 1f / attackSpeed;
            }
            _isEnemyAttack = false;
        }
        
        //Need fixing!!!!!!
        private void ObjectPushing(Transform obj, float powerForce, Vector2 direction)
        {
            obj.GetComponent<Rigidbody2D>().AddForce(direction.normalized * powerForce, ForceMode2D.Impulse);
        }
        //
        private IEnumerator EnemyColorChanging()
        {
            var color = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.15f);
            GetComponent<SpriteRenderer>().color = color;
            StartCoroutine(EnemyStunning());
        }
        //
        private IEnumerator EnemyDie(GameObject enemy)
        {
            if (enemyHealth <= 0)
            {
                enemy.GetComponent<Collider2D>().enabled = false;
                enemy.GetComponent<Enemy>().enabled = false;
                enemy.SetActive(false);
                print("Enemy die");
            }
            yield return null;
        }
        private void EnemyDie(GameObject enemy, Animator animator)
        {
            if (enemyHealth <= 0)
            {
                animator.SetTrigger("Die");
                enemy.GetComponent<Collider2D>().enabled = false;
                enemy.GetComponent<Enemy>().enabled = false;
                enemy.SetActive(false);
                print("Enemy die");
            }
        }

        private IEnumerator EnemyPatrolRight()
        {
            _animator.SetInteger("AnimState", 2);
            
            isEnemyAngry = false;
            transform.rotation = Quaternion.AngleAxis(180, Vector3.down);
            Vector3 patrolEdgeRight = patrolPoint.position;
            patrolEdgeRight += new Vector3(patrolRange, 0, 0);
            patrolEdgeRight.y = 0;
            while (transform.position.x < patrolPoint.position.x + patrolRange)
            {
                transform.position += Vector3.MoveTowards(Vector3.zero, patrolEdgeRight, patrolSpeed * 0.04f);
                yield return null;
            }
            _animator.SetInteger("AnimState", 0);
            yield return new WaitForSeconds(1.5f);
            CoroutineMemoryStart(EnemyPatrolLeft());
        }
        private IEnumerator EnemyPatrolLeft()
        {
            _animator.SetInteger("AnimState", 2);
            
            isEnemyAngry = false;
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            Vector3 patrolEdgeLeft = patrolPoint.position;
            patrolEdgeLeft -= new Vector3(patrolRange, 0, 0);
            patrolEdgeLeft.y = 0;
            while (transform.position.x > patrolPoint.position.x - patrolRange)
            {
                transform.position -= Vector3.MoveTowards(Vector3.zero, patrolEdgeLeft, patrolSpeed * 0.04f);
                yield return null;
            }
            _animator.SetInteger("AnimState", 0);
            yield return new WaitForSeconds(1.5f);
            CoroutineMemoryStart(EnemyPatrolRight());
        }

        public void RaiseTheAlarm(GameObject player)
        {
            GlobalEventManager.RaiseAlarm(player);
        }
        private void ReactionToAlarm(GameObject player)
        {
            if (_enemyDataCoroutine != null) StopCoroutine(_enemyDataCoroutine);
            print(2);
        }
        private void FalseAlarm()
        {
            isEnemyAngry = false;
        }

        
        //New system
        public IEnumerator EnemyFightReaction(GameObject player)
        {
            StopCoroutine(_enemyDataCoroutine);
            isEnemyAngry = true;

            StartCoroutine(EnemyDistanceShorten(player));
            yield return null;
        }

        private IEnumerator EnemyDistanceShorten(GameObject player)
        {
            var playerPosition = player.transform.position;
            var enemyPosition = gameObject.transform.position;
            var startPos = new Vector3(enemyPosition.x, enemyPosition.y, 0);
            var endPos = new Vector3(playerPosition.x, enemyPosition.y, 0);
            var endPosForDistance = new Vector3(playerPosition.x, enemyPosition.y, 0); ;
            _isEnemyInAccessible = true;
            while (_isEnemyInAccessible)
            {
                playerPosition = player.transform.position;
                enemyPosition = gameObject.transform.position;
                startPos = new Vector3(enemyPosition.x, enemyPosition.y, 0);
                
                if (playerPosition.x > enemyPosition.x)
                {
                    transform.rotation = Quaternion.AngleAxis(180, Vector3.down);
                    endPos = new Vector3(playerPosition.x - attackRange + 0.01f, enemyPosition.y, 0);
                }
                else if (playerPosition.x < enemyPosition.x)
                {
                    transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
                    endPos = new Vector3(playerPosition.x + attackRange - 0.01f, enemyPosition.y, 0);
                }

                var distance = playerDistanceToEnemy(startPos, endPosForDistance);

                if (distance > attackRange && !_isEnemyStun)
                {
                    _animator.SetInteger("AnimState", 2);
                    transform.position = Vector3.MoveTowards(startPos, endPos, enemyFollowingSpeed);
                    startPos = new Vector3(enemyPosition.x, enemyPosition.y, 0);
                }
                
                endPosForDistance = new Vector3(playerPosition.x, enemyPosition.y, 0);
                distance = playerDistanceToEnemy(startPos, endPosForDistance);
                
                if (distance <= attackRange && !_isEnemyStun)
                {
                    _animator.SetInteger("AnimState", 1);
                    if (!_isEnemyAttack && Time.time >= _nextAttackTime)
                    {
                        _animator.SetTrigger("Attack");
                        _isEnemyAttack = true;
                    }
                }
                
                if (distance > 5)
                {
                    _isEnemyInAccessible = false;
                    StopAllCoroutines();
                    StartPatrol();
                }
                yield return null;
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(interactionPoint.position, attackRange);
        }

        private void CoroutineMemoryStart(IEnumerator data)
        {
            _enemyDataCoroutine = data;
            StartCoroutine(data);
        }

        private IEnumerator GlobalCoroutine()
        {
            for (int i = 0; i < 2; i++)
                RegularCoroutine(i).ParallelCoroutinesGroup(this, "test");

            while (CoroutineExtension.GroupProcessing("test"))
                yield return null;

            //Debug.Log("Group 1 finished");
        }

        private IEnumerator RegularCoroutine(int id)
        {
            int iterationsCount = Random.Range(1, 5);

            for (int i = 1; i <= iterationsCount; i++)
            {
                yield return new WaitForSeconds(1);
            }
            //Debug.Log(string.Format("{0}: Coroutine {1} finished", Time.realtimeSinceStartup, id));
        }
    }
}
