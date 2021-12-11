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
        StartCoroutine(EnemyColorChanging());
        enemyHealth -= (int)damage;
        EnemyDie(gameObject);
    }

    private IEnumerator EnemyColorChanging()
    {
        var color = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = color;
    }
    private void EnemyDie(GameObject enemy)
    {
        if (enemyHealth <= 0)
        {
            enemy.GetComponent<Collider2D>().enabled = false;
            enemy.GetComponent<Enemy>().enabled = false;
            enemy.SetActive(false);
            print("Enemy die");
        }
    }
    private void EnemyDie(GameObject enemy, Animator animator)
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
    public IEnumerator EnemyPatrolRight(Transform patrolPoint, float patrolRange, float patrolSpeed)
    {
        Vector3 patrolEdgeRight = patrolPoint.position;
        patrolEdgeRight += new Vector3(patrolRange, 0, 0);
        patrolEdgeRight.y = 0;
        while (transform.position.x < patrolPoint.position.x + patrolRange)
        {
            transform.position += Vector3.Lerp(Vector3.zero, patrolEdgeRight, patrolSpeed * 0.04f);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(EnemyPatrolLeft(patrolPoint, patrolRange, patrolSpeed));
    }
    private IEnumerator EnemyPatrolLeft(Transform patrolPoint, float patrolRange, float patrolSpeed)
    {
        Vector3 patrolEdgeLeft = patrolPoint.position;
        patrolEdgeLeft -= new Vector3(patrolRange, 0, 0);
        patrolEdgeLeft.y = 0;
        while (transform.position.x > patrolPoint.position.x - patrolRange)
        {
            transform.position -= Vector3.Lerp(Vector3.zero, patrolEdgeLeft, patrolSpeed * 0.04f);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(EnemyPatrolRight(patrolPoint, patrolRange, patrolSpeed));
    }
}
