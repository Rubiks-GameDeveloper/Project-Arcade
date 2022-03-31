using AdditionalMethods;
using UnityEngine;

namespace FightSystem.EnemyStates
{
    public class EnemyGroundedState : State
    {
        private Transform _enemyPosition;
        protected bool IsEnemyGrounded;
        public EnemyGroundedState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            IsEnemyGrounded = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Collider2D[] colliders = new Collider2D[25];
            var size = new Vector2(enemy.groundCheckerRange, 0.2f);
            var res = Physics2D.OverlapBoxNonAlloc(enemy.groundChecker.position, size, 0, colliders);
            if (res > 1)
            {
                foreach (var item in colliders)
                {
                    if (item != null && item.gameObject.CompareTag("Ground"))
                    {
                        if (!IsEnemyGrounded)
                        {
                            IsEnemyGrounded = true;
                        }
                        return;
                    }
                    IsEnemyGrounded = false;
                }
            }
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
}