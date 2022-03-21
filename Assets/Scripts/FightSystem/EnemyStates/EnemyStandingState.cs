using AdditionalMethods;

namespace FightSystem.EnemyStates
{
    public class EnemyStandingState : EnemyGroundedState
    {
        protected bool IsEnemyPatrol;

        protected EnemyStandingState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        
        }

        public override void Enter()
        {
            base.Enter();
            IsEnemyPatrol = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (IsEnemyGrounded && !IsEnemyPatrol && !enemy.EnemyAngryState.IsEnemyAngry)
            {
                stateMachine.ChangeState(enemy.EnemyPatrolState);
                IsEnemyPatrol = true;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
