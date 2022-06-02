using System;
using System.Collections;
using AdditionalMethods;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FightSystem.EnemyStates
{
    public class EnemyPatrolState : EnemyStandingState
    {
        private bool _isEnemyPatrolRight;
        private bool _isEnemyPatrolLeft;

        private Vector3 _patrolEdgeRight;
        private Vector3 _patrolEdgeLeft;
        public EnemyPatrolState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            if (!_isEnemyPatrolLeft && !_isEnemyPatrolRight)
            {
                StartPatrol();
                //_patrolEdgeRight = enemy.patrolPoint.position;
                //_patrolEdgeRight += new Vector3(enemy.patrolRange, 0, 0);
                _patrolEdgeRight.y = 0;
            
                //_patrolEdgeLeft = enemy.patrolPoint.position;
                //_patrolEdgeLeft -= new Vector3(enemy.patrolRange, 0, 0);
                _patrolEdgeLeft.y = 0;
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (_isEnemyPatrolLeft && !enemy.isEnemyWait && !enemy.isEnemyStun) EnemyPatrolLeft();
            else if (_isEnemyPatrolRight && !enemy.isEnemyWait && !enemy.isEnemyStun) EnemyPatrolRight();
        }

        public override void Exit()
        {
            base.Exit();
            _isEnemyPatrolLeft = false;
            _isEnemyPatrolRight = false;
            
            _patrolEdgeRight = Vector3.zero;
            _patrolEdgeLeft = Vector3.zero;
            
            enemy.StopAllCoroutines();
        }
        
        private void StartPatrol()
        {
            var number = Random.Range(0, 2);
            switch (number)
            {
                case 0:
                {
                    _isEnemyPatrolLeft = true;
                    enemy.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
                    break;
                }
                case 1:
                {
                    _isEnemyPatrolRight = true;
                    enemy.transform.rotation = Quaternion.AngleAxis(180, Vector3.down);
                    break;
                }
            }
        }
        private void EnemyPatrolRight()
        {
            enemy.animator.SetInteger("AnimState", 2);
            enemy.transform.rotation = Quaternion.AngleAxis(180, Vector3.down);
            /*
            if (enemy.transform.position.x < enemy.patrolPoint.position.x + enemy.patrolRange)
            {
                enemy.vectorMovement.Move(_patrolEdgeRight, enemy.patrolSpeed);
            }
            else
            {
                enemy.animator.SetInteger("AnimState", 0);
                _isEnemyPatrolRight = false;
                _isEnemyPatrolLeft = true;
                enemy.StartCoroutine(enemy.EnemyWaiting(enemy.patrolWaitTime));
            }*/
        }
        private void EnemyPatrolLeft()
        {
            enemy.animator.SetInteger("AnimState", 2);
            enemy.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            /*
            if (enemy.transform.position.x > enemy.patrolPoint.position.x - enemy.patrolRange)
            {
                enemy.vectorMovement.Move(-_patrolEdgeLeft, enemy.patrolSpeed);
            }
            else
            {
                enemy.animator.SetInteger("AnimState", 0);
                _isEnemyPatrolLeft = false;
                _isEnemyPatrolRight = true;
                enemy.StartCoroutine(enemy.EnemyWaiting(enemy.patrolWaitTime));
            }*/
        }
    }
}