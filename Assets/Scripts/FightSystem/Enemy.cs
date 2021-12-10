using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public int enemyHealth;
    public int enemyArmor;

    public void DamageTaking(float damage, Animator animator)
    {
        enemyHealth += (int)(enemyArmor * 0.3f);
        enemyHealth -= (int)damage;
        animator.SetTrigger("DamageTaking");
        EnemyDie(animator.gameObject, animator);
    }
    public void DamageTaking(float damage)
    {
        enemyHealth -= (int)damage;
        EnemyDie(gameObject);
    }
    public void EnemyDie(GameObject enemy)
    {
        if (enemyHealth <= 0)
        {
            enemy.GetComponent<Collider2D>().enabled = false;
            enemy.GetComponent<Enemy>().enabled = false;
            enemy.SetActive(false);
            print("Enemy die");
        }
    }
    public void EnemyDie(GameObject enemy, Animator animator)
    {
        if (enemyHealth <= 0)
        {
            animator.SetTrigger("Die");
            enemy.GetComponent<Collider2D>().enabled = false;
            enemy.GetComponent<Enemy>().enabled = false;
            enemy.SetActive(false);
            print("Enemy die");
        }
    }
}
