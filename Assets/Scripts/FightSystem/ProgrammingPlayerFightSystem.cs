using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FightSystem
{
    public class ProgrammingPlayerFightSystem : MonoBehaviour
    {
        [SerializeField] private float hurtAnimationLength;
    
        [SerializeField] private float playerDamage;
        [SerializeField] private float attackRange;
        [SerializeField] private float playerAttackSpeed;
        [SerializeField] private float playerAttackTime;
        private float _nextAttackTime;
        [SerializeField] private GameObject attackPoint;
        [SerializeField] private LayerMask enemyLayer;
        public float maxPlayerHealth;
        private float _currentPlayerHealth;
    
        private Animator _playerAnimator;

        public bool isPlayerAttack;
        public bool isPlayerStun;

        [SerializeField] private Image healthBar;
        [SerializeField] private GameObject dieScreen;

        public bool isPlayerBlock;
        private void Start()
        {
            _currentPlayerHealth = maxPlayerHealth;
            _playerAnimator = GetComponent<Animator>();
            HealthBarUpdate(healthBar);
        }
        public void PlayerAttack()
        {
            if (Time.time >= _nextAttackTime && !isPlayerStun)
            {
                StartCoroutine(PlayerDontMove());
                _playerAnimator.SetTrigger("Attack1");
                Collider2D[] enemy = new Collider2D[20];
                Physics2D.OverlapCircleNonAlloc(attackPoint.transform.position, attackRange, enemy, enemyLayer.value);
                foreach (Collider2D dataEnemy in enemy)
                {
                    if (dataEnemy != null && !dataEnemy.CompareTag("EnemyReactionTrigger"))
                    {
                        StartCoroutine(dataEnemy.GetComponent<Enemy>().DamageTaking(playerDamage));
                        print(dataEnemy.name);
                    }
                }
                _nextAttackTime = Time.time + 1f / playerAttackSpeed;
            }
        }
        public void PlayerDamageTaking(float damage)
        {
            if (maxPlayerHealth > 0) _currentPlayerHealth -= damage;
            HealthBarUpdate(healthBar);
            PlayerDeathAnimation();
        }
        private void PlayerDeathAnimation()
        {
            if (_currentPlayerHealth <= 0)
            {
                dieScreen.SetActive(true);
                _playerAnimator.SetBool("Death", true);
            }
            else
            {
                _playerAnimator.SetTrigger("Hurt");
                StartCoroutine(PlayerStunning());
            }
        }
        private IEnumerator PlayerStunning()
        {
            isPlayerStun = true;
            yield return new WaitForSeconds(hurtAnimationLength);
            isPlayerStun = false;
        }

        public void PlayerBlock()
        {
            if (!isPlayerBlock)
            {
                isPlayerBlock = true;
                _playerAnimator.SetInteger("State", 1);
            }
            else
            {
                isPlayerBlock = false;
                _playerAnimator.SetInteger("State", 0);
            }
        }
        public void Death()
        {
            GetComponent<PlayerProgrammingTransformer>().enabled = false;
            GetComponent<ProgrammingPlayerFightSystem>().enabled = false;
            GetComponent<Rigidbody2D>().Sleep();
            _playerAnimator.enabled = false;
            Time.timeScale = 0;
        }
        private void HealthBarUpdate(Image healthBar)
        {
            healthBar.fillAmount = 1f / maxPlayerHealth * _currentPlayerHealth;
        }
        private IEnumerator PlayerDontMove()
        {
            isPlayerAttack = true;
            yield return new WaitForSeconds(playerAttackTime);
            isPlayerAttack = false;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
        }
    }
}
