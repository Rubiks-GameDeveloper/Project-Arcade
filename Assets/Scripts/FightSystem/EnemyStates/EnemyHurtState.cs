using AdditionalMethods;
using UnityEngine;

namespace FightSystem.EnemyStates
{
    public class EnemyHurtState: EnemyStandingState
    {
        private float _playerDamage;
        private float _stunTime;

        public EnemyHurtState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _playerDamage = enemy.playerDamage;
            _stunTime = enemy.stunTime;
            DamageTaking();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Exit()
        {
            base.Exit();
        }

        private void DamageTaking()
        {
            enemy.isEnemyAttack = false;
            enemy.animator.SetTrigger("Hurt");
            enemy.enemyHealth -= (int)_playerDamage;
            enemy.StartCoroutine(enemy.EnemyStunning(_stunTime));
            stateMachine.ChangeState(enemy.EnemyAngryState);
            enemy.EnemyDie();
        }
    }
}