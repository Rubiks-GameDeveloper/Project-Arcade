using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    
    public int enemyHealth;
    public int enemyArmor;
    private float _nextAttackTime;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float enemyDamage;
    [SerializeField] private float attackTime;
    
    [Header("Values for patrol")]
    [SerializeField] private Transform patrolPoint;
    [SerializeField] private float patrolRange;
    [SerializeField] private float patrolSpeed;
    
    [Header("Values for reacting on events")]
    [SerializeField] private float enemyFollowingSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float pushingForce;
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float stunTime;
    
    public bool isEnemyAngry;
    private bool _isEnemyStun;
    public float playerDistanceToEnemy(Transform enemy, Transform player)
    {
        Vector2 distance = player.position - enemy.position;
        return (float)Math.Sqrt(Math.Pow(distance.x, 2) + Math.Pow(distance.y, 2));
    }
    public void DamageTaking(float damage, Animator animator)
    {
        enemyHealth += (int)(enemyArmor * 0.3f);
        enemyHealth -= (int)damage;
        animator.SetTrigger("DamageTaking");
        EnemyDie(animator.gameObject, animator);
        StartCoroutine(EnemyStunning());
    }
    public void DamageTaking(float damage)
    {
        StartCoroutine(EnemyColorChanging());
        enemyHealth -= (int)damage;
        EnemyDie(gameObject);
        //StopAllCoroutines();
        //StartCoroutine(EnemyStunning());
    }

    private IEnumerator EnemyStunning()
    {
        _isEnemyStun = true;
        yield return new WaitForSeconds(stunTime);
        _isEnemyStun = false;
    }
    private void PlayerFollowing(Transform player)
    {
        if (!isEnemyAngry) StopAllCoroutines();
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(player.position.x, startPos.y);
        transform.position = Vector3.MoveTowards(startPos, endPos, enemyFollowingSpeed * 0.2f);
        isEnemyAngry = true;
    }
    public void EnemyReacting(float distance, float reactionRange, Collider2D playerInRange, Transform player)
    {
        if (Time.time >= _nextAttackTime && !_isEnemyStun)
        {
            if (distance <= attackRange && playerInRange != null) 
                StartCoroutine(Attack(playerInRange));
            else if (distance <= reactionRange && distance > attackRange) 
                PlayerFollowing(player.transform);
            else if (distance > reactionRange && isEnemyAngry)
                StartCoroutine(EnemyPatrolRight());
        }
    }
    private IEnumerator Attack(Collider2D player)
    {
        isEnemyAngry = true;
        yield return new WaitForSeconds(attackTime);
        if (Time.time >= _nextAttackTime && playerDistanceToEnemy(transform, player.transform) <= attackRange)
        {
            player.GetComponent<ProgrammingPlayerFightSystem>().PlayerDamageTaking(enemyDamage);
            PlayerProgrammingTransformer pl = player.GetComponent<PlayerProgrammingTransformer>();
            Vector2 direction = new Vector2(-pl.playerJoystick.horizontalConst, 0);
            ObjectPushing(player.transform, pushingForce, player.GetComponent<SurfaceCollector>().Projection(direction.normalized));
            _nextAttackTime = Time.time + 1f / attackSpeed;
        }
    }
    private void ObjectPushing(Transform obj, float powerForce, Vector2 direction)
    {
        obj.GetComponent<Rigidbody2D>().AddForce(direction.normalized * powerForce, ForceMode2D.Impulse);
    }
    //
    private IEnumerator EnemyColorChanging()
    {
        var color = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = color;
        StopAllCoroutines();
        StartCoroutine(EnemyStunning());
    }
    //
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
    public IEnumerator EnemyPatrolRight()
    {
        isEnemyAngry = false;
        Vector3 patrolEdgeRight = patrolPoint.position;
        patrolEdgeRight += new Vector3(patrolRange, 0, 0);
        patrolEdgeRight.y = 0;
        while (transform.position.x < patrolPoint.position.x + patrolRange)
        {
            transform.position += Vector3.MoveTowards(Vector3.zero, patrolEdgeRight, patrolSpeed * 0.04f);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(EnemyPatrolLeft());
    }
    private IEnumerator EnemyPatrolLeft()
    {
        isEnemyAngry = false;
        Vector3 patrolEdgeLeft = patrolPoint.position;
        patrolEdgeLeft -= new Vector3(patrolRange, 0, 0);
        patrolEdgeLeft.y = 0;
        while (transform.position.x > patrolPoint.position.x - patrolRange)
        {
            transform.position -= Vector3.MoveTowards(Vector3.zero, patrolEdgeLeft, patrolSpeed * 0.04f);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(EnemyPatrolRight());
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(interactionPoint.position, attackRange);
    }
}
