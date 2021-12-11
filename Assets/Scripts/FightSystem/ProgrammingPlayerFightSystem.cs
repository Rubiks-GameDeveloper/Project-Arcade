using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProgrammingPlayerFightSystem : MonoBehaviour
{
    [SerializeField] private float playerDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private float playerAttackSpeed;
    private float _nextAttackTime;
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private LayerMask enemyLayer;
    
    private Animator _playerAnimator;
    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }
}
