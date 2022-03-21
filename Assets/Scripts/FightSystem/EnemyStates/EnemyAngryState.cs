using System;
using AdditionalMethods;
using UnityEngine;

namespace FightSystem.EnemyStates
{
    public class EnemyAngryState: EnemyStandingState
    {
        public bool IsEnemyAngry;
        private bool _isEnemyDistanceShorten;

        private GameObject _player;
        private Animator _animator;

        private float _followingSpeed;
        private float _distanceToPlayer;
        private float _enemyAngryDistance;
        
        private float _attackRange;
        private float _nextAttackTime;

        public EnemyAngryState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            _animator = enemy.animator;
            _followingSpeed = enemy.enemyFollowingSpeed;
            _attackRange = enemy.attackRange;
            _enemyAngryDistance = enemy.enemyAngryDistance;

            IsEnemyPatrol = false;
            IsEnemyAngry = true;
            _isEnemyDistanceShorten = false;
            enemy.isEnemyWait = false;
            PlayerDetection();

            if (_enemyAngryDistance == 0) _enemyAngryDistance = 5;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (_player != null)
                _distanceToPlayer = PlayerDistanceToEnemy(enemy.transform.position, _player.transform.position);
            if (_distanceToPlayer <= enemy.playerDetectionRange) _isEnemyDistanceShorten = true;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            if (_isEnemyDistanceShorten && !enemy.isEnemyStun) DistanceShorten();
        }

        public override void Exit()
        {
            base.Exit();
            IsEnemyAngry = false;
        }

        public void Attack()
        {
            if (!enemy.isEnemyStun)
            {
                Collider2D[] player = new Collider2D[20];
                Physics2D.OverlapCircleNonAlloc(enemy.transform.position, _attackRange + 2, player);
                foreach (Collider2D dataEnemy in player)
                {
                    if (dataEnemy != null && dataEnemy.CompareTag("Player") && !enemy.isEnemyStun && enemy.isEnemyAttack)
                    {
                        var playerFightComponent = dataEnemy.GetComponent<ProgrammingPlayerFightSystem>();
                        var isPlayerBlock = dataEnemy.transform.rotation.y == enemy.transform.rotation.y &&
                                            playerFightComponent.isPlayerBlock;
                        if (!isPlayerBlock)
                        {
                            playerFightComponent.PlayerDamageTaking(enemy.enemyDamage);
                        }
                    }
                }
                _nextAttackTime = Time.time + 1f / enemy.attackSpeed;
            }
        }

        private void DistanceShorten()
        {
            var playerPosition = _player.transform.position;
            var enemyPosition = enemy.transform.position;
            var endPos = new Vector3(playerPosition.x, enemyPosition.y, 0);

            if (playerPosition.x > enemyPosition.x)
            {
                enemy.transform.rotation = Quaternion.AngleAxis(180, Vector3.down);
                endPos = new Vector3(playerPosition.x - enemy.attackRange + 0.4f, enemyPosition.y, 0);
            }
            else if (playerPosition.x < enemyPosition.x)
            {
                enemy.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
                endPos = -new Vector3(playerPosition.x + enemy.attackRange - 0.4f, enemyPosition.y, 0);
            }

            if (_distanceToPlayer > _attackRange && !enemy.isEnemyStun && !enemy.isEnemyAttack)
            {
                _animator.SetInteger("AnimState", 2);
                enemy.vectorMovement.Move(endPos, _followingSpeed);
            }
            

            else if (_distanceToPlayer <= _attackRange && !enemy.isEnemyStun)
            {
                _animator.SetInteger("AnimState", 1);
                if (!enemy.isEnemyAttack && Time.time >= _nextAttackTime  && !enemy.isEnemyStun)
                {
                    enemy.isEnemyAttack = true;
                    _animator.SetTrigger("Attack");
                }
            }
            else if (_distanceToPlayer > _enemyAngryDistance)
            {
                stateMachine.ChangeState(enemy.EnemyStandingState);
            }
        }

        private void PlayerDetection()
        {
            Collider2D[] player = new Collider2D[20];
            Physics2D.OverlapCircleNonAlloc(enemy.transform.position, enemy.playerDetectionRange, player);
            foreach (Collider2D dataEnemy in player)
            {
                if (dataEnemy != null && dataEnemy.CompareTag("Player"))
                {
                    _player = dataEnemy.gameObject;
                    break;
                }
            }
        }
        
        private float PlayerDistanceToEnemy(Vector3 enemyPos, Vector3 player)
        {
            Vector2 distance = player - enemyPos;
            return (float)Math.Sqrt(Math.Pow(distance.x, 2) + Math.Pow(distance.y, 2));
        }
    }
}