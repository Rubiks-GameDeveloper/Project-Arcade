using System.Collections;
using AdditionalMethods;
using FightSystem.EnemyStates;
using UnityEngine;
using FluentBehaviourTree;

namespace FightSystem
{
    public class Enemy : MonoBehaviour
    {
        public Transform groundChecker;
        public float groundCheckerRange;
    
        public int enemyHealth;
        public int enemyArmor;
        private float _nextAttackTime;
        public float attackSpeed;
        public float enemyDamage;
        public float enemyAngryDistance;

        public VectorMovement vectorMovement;

        [Header("Values for patrol")] 
        [SerializeField] private Transform patrolPointLeft;
        [SerializeField] private Transform patrolPointRight;
        public float patrolWaitTime;
        [Range(0.01f, 1.5f)] public float patrolSpeed;
    
        [Header("Values for reacting on events")]
        [Range(0.01f, 2.5f)] public float enemyFollowingSpeed; 
        public float attackRange;
        public float pushingForce;
        [SerializeField] private Transform interactionPoint;
        public float stunTime;

        #region OldProperty
        public float playerDetectionRange;

        public Animator animator;

        private StateMachine _stateMachine;
        
        //All enemy states 
        private EnemyGroundedState _enemyGroundedState;
        public EnemyStandingState EnemyStandingState;
        public EnemyPatrolState EnemyPatrolState;
        public EnemyAngryState EnemyAngryState;
        private EnemyHurtState _enemyHurtState;
        
        
        public bool isEnemyStun;
        public bool isEnemyWait;
        public bool isEnemyAttack;
        
        private State _isEnemyInAccessible;
        private State _isEnemyAttack;

        public float playerDamage;

        private IEnumerator _enemyDataCoroutine;

        private readonly StateMachine _enemyStateMachine = new StateMachine();
        private static readonly int Death = Animator.StringToHash("Death");
        #endregion
        #region OldRealization
        private void Awake()
        {
            /*_stateMachine = new StateMachine();

            EnemyAngryState = new EnemyAngryState(this, _stateMachine);
            _enemyHurtState = new EnemyHurtState(this, _stateMachine);
            _enemyGroundedState = new EnemyGroundedState(this, _stateMachine);
            EnemyStandingState = new EnemyPatrolState(this, _stateMachine);
            EnemyPatrolState = new EnemyPatrolState(this, _stateMachine);

            animator = GetComponent<Animator>();*/
            Startup();
        }
        private void Start()
        {
            //_stateMachine.Initialize(EnemyStandingState);
        }
        /*private void FixedUpdate()
        {
            _stateMachine.CurrentState.HandleInput();
            _stateMachine.CurrentState.LogicUpdate();
            _stateMachine.CurrentState.PhysicsUpdate();
        }*/
        public void EnemyAngryStateActivate()
        {
            _stateMachine.ChangeState(EnemyAngryState);
        }
        public void DamageTaking(float damage)
        {
            playerDamage = damage;
            _stateMachine.ChangeState(_enemyHurtState);
        }
        public void EnemyDisabling()
        {
            gameObject.SetActive(false);
        }
        public IEnumerator EnemyStunning(float stunClock)
        {
            isEnemyStun = true;
            yield return new WaitForSeconds(stunClock);
            isEnemyStun = false;
        }
        public IEnumerator EnemyWaiting(float waitTime)
        {
            animator.SetInteger("AnimState", 0);
            isEnemyWait = true;
            yield return new WaitForSeconds(waitTime);
            isEnemyWait = false;
        }
        private void Damage()
        {
            EnemyAngryState.Attack();
        }
        private void AttackComplete()
        {
            isEnemyAttack = false;
        }
        private void ObjectPushing(Transform obj, float powerForce)
        {
            if (transform.rotation.y == 0)
            {
                obj.GetComponent<Rigidbody2D>().AddForce(Vector2.left * powerForce, ForceMode2D.Impulse);
            }
            else
            {
                obj.GetComponent<Rigidbody2D>().AddForce(Vector2.right * powerForce, ForceMode2D.Impulse);
            }
        }
        private IEnumerator EnemyColorChanging()
        {
            var color = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.15f);
            GetComponent<SpriteRenderer>().color = color;
            StartCoroutine(EnemyStunning(stunTime));
        }
        public void EnemyDie()
        {
            if (enemyHealth <= 0)
            {
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<Collider2D>().enabled = false;
                animator.SetTrigger(Death);
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
        }

        #endregion
        
        IBehaviourTreeNode _treeNode;
        private bool _isEnemyPatrol;
        private bool _isEnemyPatrolLeft;
        private bool _isEnemyPatrolRight;

        public void Startup()
        {
            var builder = new BehaviourTreeBuilder();
            _treeNode = builder
                .Selector("EnemyPatrol")
                .Condition("IsBlockageInVision", t => IsBlockageInVision())
                .Do("Move", t =>
                {
                    return BehaviourTreeStatus.Running;
                })
                .End()
                .Build();

        }

        private void Update()
        {
            _treeNode.Tick(new TimeData(Time.deltaTime));
        }

        private BehaviourTreeStatus EnemyPatrol()
        {
            

            return BehaviourTreeStatus.Running;
        }

        private bool IsBlockageInVision()
        {
            return true;
        }
    }
}
