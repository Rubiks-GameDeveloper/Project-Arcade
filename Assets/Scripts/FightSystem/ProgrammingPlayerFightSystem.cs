using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgrammingPlayerFightSystem : MonoBehaviour
{
    [SerializeField] private float playerDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private float playerAttackSpeed;
    private float _nextAttackTime;
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private LayerMask enemyLayer;
    public float maxPlayerHealth;
    private float _currentPlayerHealth;
    
    private Animator _playerAnimator;

    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject dieScreen;
    private void Start()
    {
        _currentPlayerHealth = maxPlayerHealth;
        _playerAnimator = GetComponent<Animator>();
        HealthBarUpdate(healthBar);
    }
    public void PlayerAttack()
    {
        if (Time.time >= _nextAttackTime)
        {
            _playerAnimator.SetTrigger("Attack1");
            Collider2D[] enemy = new Collider2D[20];
            Physics2D.OverlapCircleNonAlloc(attackPoint.transform.position, attackRange, enemy, enemyLayer.value);
            foreach (Collider2D dataEnemy in enemy)
            {
                if (dataEnemy != null) dataEnemy.GetComponent<Enemy>().DamageTaking(playerDamage);
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
            _playerAnimator.SetBool("Death", true);
        }
        else
        {
            _playerAnimator.SetTrigger("Hurt");
        }
    }
    public void Death()
    {
        GetComponent<PlayerProgrammingTransformer>().enabled = false;
        GetComponent<ProgrammingPlayerFightSystem>().enabled = false;
        _playerAnimator.enabled = false;
        dieScreen.SetActive(true);
    }
    private void HealthBarUpdate(Image healthBar)
    {
        healthBar.fillAmount = 1f / maxPlayerHealth * _currentPlayerHealth;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }
}
