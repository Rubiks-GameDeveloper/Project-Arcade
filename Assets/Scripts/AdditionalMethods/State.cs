using System;
using FightSystem;
using UnityEngine;

namespace AdditionalMethods
{
    public abstract class State
    {
        protected readonly Enemy enemy;
        protected readonly StateMachine stateMachine;
        
        protected State(Enemy enemy, StateMachine stateMachine)
        {
            this.enemy = enemy;
            this.stateMachine = stateMachine;
        }
        public virtual void Enter()
        {
            
        }
        public virtual void HandleInput()
        {
            
        }
        public virtual void LogicUpdate()
        {
            
        }
        public virtual void PhysicsUpdate()
        {
            
        }
        public virtual void Exit()
        {
            
        }
    }
}
