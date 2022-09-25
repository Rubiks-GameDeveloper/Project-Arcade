using System.Collections;
using UnityEngine;
using FluentBehaviourTree;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace FightSystem
{
    [RequireComponent(typeof(VectorMovement))]
    [RequireComponent(typeof(SurfaceCollector))]
    public class Enemy : MonoBehaviour
    {
        [Header("Fight property")]
        [SerializeField] private AttackType attackType = AttackType.Melee;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float projectileLifetime = 5;
        [SerializeField] private float projectileSpeed = 2;
        [SerializeField] private float pushingForce;
        private GameObject _projectileData;
        public int enemyHealth;
        public int enemyArmor;
        private float _nextAttackTime;
        public float attackSpeed;
        public float enemyDamage;
        public float attackRange;
        private float _playerDamage;
        [Space]
        
        [Header("Movement")]
        [SerializeField] private float jumpForce = 15;
        [Range(0.01f, 1.5f)] public float patrolSpeed;
        [SerializeField] private float patrolRange = 0;
        public VectorMovement vectorMovement;
        private RotateClass _rotateClass;
        private IBehaviourTreeNode _treeNode;
        private PrototypeJump _prototypeJump;
        [Space]
        
        [Header("Reaction range")]
        [SerializeField] private EnemyReacting reactingToPlayer;
        [SerializeField] private GameObject reactionTrigger;
        public float patrolWaitTime;
        public float playerDetectionRange;
        private Rigidbody2D _rb;
        private GameObject _player;
        [Space]
        
        [Header("Graphics")]
        public Animator animator;
        [SerializeField] private ParticleSystem runningParticles;
        [Space]
        
        private bool _isPatrollingLeft = true;
        private bool _isPatrollingRight;
        private bool _isWaiting;

        private Vector3 _startPosition;
        private Vector3 _currentMoveDirection;

        private static readonly int Death = Animator.StringToHash("Death");
        private void Awake()
        {
            animator = GetComponent<Animator>();
            switch (attackType)
            {
                case AttackType.Melee:
                    StartupMelee();
                    break;
                case AttackType.Range:
                    StartupRange();
                    break;
                default:
                    StartupMelee();
                    break;
            }
        }
        private void Start()
        {
            _rotateClass = new RotateClass(gameObject);
            _rb = GetComponent<Rigidbody2D>();
            if (attackType == AttackType.Range) reactionTrigger.SetActive(false);
            _startPosition = transform.position;
        }
        public void DamageTaking(float damage)
        {
            _playerDamage = damage;
            enemyHealth -= (int)_playerDamage;
            if (enemyHealth <= 0) animator.SetTrigger(Death);
        }
        public void EnemyDisabling()
        {
            Destroy(gameObject);
        }
        private void StartupRange()
        {
            var builder = new BehaviourTreeBuilder();
            _treeNode = builder
                .Selector("Player in Attack range")
                    .Condition("Is Player in Attack range?", t => !IsPlayerInVision())
                        .Do("Throw the projectile", t => PrepareToAttack())
                    .End()
                .Build();
        }
        private void StartupMelee()
        {
            var builder = new BehaviourTreeBuilder();
            _treeNode = builder
                .Parallel("All", 1, 0)
                    .Do("Patrolling", t => EnemyStatusPatrolling())
                    .Selector("Player attack range")
                        .Condition("Is player in attack range", t => !IsPlayerInAttackRange())
                        .Do("Attack", t => PrepareToAttack())
                    .End()
                .End()
                .Build();
        }
        private void FixedUpdate()
        {
            if (attackType == AttackType.Melee) IsPlayerInVision();
            _treeNode.Tick(new TimeData(Time.deltaTime));
        }
        private IEnumerator PatrolWait(int previouslyDirection)
        {
            _isWaiting = true;
            animator.SetInteger("AnimState", 0);

            yield return new WaitForSeconds(patrolWaitTime);
            switch (previouslyDirection)
            {
                case 1:
                    _rotateClass.TurnRight();
                    _isPatrollingRight = true;
                    _isPatrollingLeft = false;
                    _isWaiting = false;
                    break;
                case -1:
                    _rotateClass.TurnLeft();
                    _isPatrollingLeft = true;
                    _isPatrollingRight = false;
                    _isWaiting = false;
                    break;
            }
        }
        private void EnemyPatrolLeft()
        {
            if (_startPosition.x - patrolRange < transform.position.x)
            {
                animator.SetInteger("AnimState", 2);
                vectorMovement.Move(Vector3.left, patrolSpeed);
            }
            else
            {
                _isPatrollingLeft = false;
                if (!_isWaiting) StartCoroutine(PatrolWait(1));
                animator.SetInteger("AnimState", 0);
            }
        }
        private void EnemyPatrolRight()
        {
            if (_startPosition.x + patrolRange > transform.position.x)
            {
                animator.SetInteger("AnimState", 2);
                vectorMovement.Move(Vector3.right, patrolSpeed);
            }
            else
            {
                _isPatrollingRight = false;
                if (!_isWaiting) StartCoroutine(PatrolWait(-1));
                animator.SetInteger("AnimState", 0);
            }
        }
        private BehaviourTreeStatus EnemyStatusPatrolling()
        {
            if (IsPlayerInAttackRange())
            {
                return BehaviourTreeStatus.Failure;
            }

            if (_isPatrollingLeft && !_isPatrollingRight && !_isWaiting)
            {
                EnemyPatrolLeft();
            }
            else if (_isPatrollingRight && !_isPatrollingLeft && !_isWaiting) EnemyPatrolRight();

            return BehaviourTreeStatus.Running;
        }
        private BehaviourTreeStatus PrepareToAttack()
        {
            
            animator.SetInteger("AnimState", 1);
            if (attackType == AttackType.Range && _nextAttackTime <= Time.time) Attack();
            else if (_nextAttackTime <= Time.time && IsPlayerInAttackRange()) animator.SetTrigger("Attack");
            return BehaviourTreeStatus.Success;
        }
        // ReSharper disable once MemberCanBePrivate.Global
        // Its using in animation event!
        public void Attack()
        {
            if (attackType == AttackType.Melee)
            {
                var player = new Collider2D[20];
                Physics2D.OverlapCircleNonAlloc(transform.position, attackRange, player);
                foreach (var dataEnemy in player)
                    if (dataEnemy != null && dataEnemy.gameObject.layer == 7)
                    {
                        var playerFightComponent = dataEnemy.GetComponent<ProgrammingPlayerFightSystem>();
                        var isPlayerBlock =
                            dataEnemy.GetComponent<PlayerProgrammingTransformer>().RotateClass.CurrentRotationState !=
                            _rotateClass.CurrentRotationState &&
                            playerFightComponent.isPlayerBlock;
                        //if (!isPlayerBlock) continue;
                        playerFightComponent.PlayerDamageTaking(enemyDamage);
                    }
            }
            else
            {
                var startPosition = transform.position + Vector3.up;
                if (_player.transform.position.x < transform.position.x)
                {
                    _rotateClass.TurnLeft();
                    startPosition += Vector3.left;
                }
                else
                {
                    _rotateClass.TurnRight();
                    startPosition += Vector3.right;
                }
                _projectileData = Instantiate(projectilePrefab, startPosition, Quaternion.identity);
                var data = _projectileData.GetComponent<Projectile>();
                data.direction = _player.transform.position.x < transform.position.x ? ProjectileDirection.Left : ProjectileDirection.Right;
                data.damage = enemyDamage;
                data.projectileLifetime = projectileLifetime;
                data.projectileSpeed = projectileSpeed;
            }

            _nextAttackTime = Time.time + 1f / attackSpeed;
        }
        private bool IsPlayerInAttackRange()
        {
            if (reactingToPlayer.player != null)
            {
                if (_player.transform.position.x < transform.position.x && _rotateClass.CurrentRotationState != 1)
                {
                    _rotateClass.TurnLeft();
                }
                else if (_player.transform.position.x > transform.position.x && _rotateClass.CurrentRotationState != -1)
                {
                    _rotateClass.TurnRight();
                }
                return Vector2.Distance(transform.position, _player.transform.position) <=
                       attackRange;
            }
            if (attackType != AttackType.Melee) return false;
            if (_isPatrollingLeft && _rotateClass.CurrentRotationState != 1)
            {
                _rotateClass.TurnLeft();
            }
            else if (_isPatrollingRight && _rotateClass.CurrentRotationState != -1)
            {
                _rotateClass.TurnRight();
            }
            return false;
        }
        private bool IsPlayerInVision()
        {
            var size = new Collider2D[25];
            Physics2D.OverlapCircleNonAlloc(transform.position, playerDetectionRange, size);
            foreach (var item in size)
            {
                if (item != null && item.gameObject.CompareTag("Player"))
                {
                    _player = item.gameObject;
                    return true;
                }
            }
            return false;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.grey;
            var position = transform.position;
            Gizmos.DrawWireSphere(position, attackRange);
            Gizmos.color = Color.cyan;
            if (attackType != AttackType.Melee) Gizmos.DrawWireSphere(position, playerDetectionRange);
        }
    }
    public enum AttackType {Melee, Range}
}
