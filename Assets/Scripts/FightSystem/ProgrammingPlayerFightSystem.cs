using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProgrammingPlayerFightSystem : MonoBehaviour
{
    [SerializeField] private float playerDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private LayerMask enemyLayer;
    
    private Animator playerAnimator;
    private void Start()
    {
        //playerAnimator = GetComponent<Animator>();
    }

    public void PlayerAttack()
    {
        //playerAnimator.SetTrigger("Attack");
        
        Collider2D[] enemy = new Collider2D[20];
        Physics2D.OverlapCircleNonAlloc(attackPoint.transform.position, attackRange, enemy, enemyLayer.value);
        foreach (Collider2D dataEnemy in enemy)
        {
            if (dataEnemy != null) dataEnemy.GetComponent<Enemy>().DamageTaking(playerDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }
}
