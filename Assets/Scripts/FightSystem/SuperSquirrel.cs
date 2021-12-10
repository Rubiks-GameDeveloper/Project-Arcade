using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SuperSquirrel : MonoBehaviour
{
    private Enemy sSqirrel;
    [SerializeField] private Transform patrolPoint;
    [SerializeField] private float patrolRange;
    [SerializeField] private float patrolSpeed;
    private void Start()
    {
        sSqirrel = GetComponent<Enemy>();
        StartCoroutine(EnemyPatrolRight());
    }
    

    private IEnumerator EnemyPatrolRight()
    {
        Vector3 patrolEdgeRight = patrolPoint.position;
        patrolEdgeRight += new Vector3(patrolRange, 0, 0);
        patrolEdgeRight.y = 0;
        while (transform.position.x < patrolPoint.position.x + patrolRange)
        {
            transform.position += Vector3.Lerp(Vector3.zero, patrolEdgeRight, Time.fixedDeltaTime * patrolSpeed);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(EnemyPatrolLeft());
    }
    private IEnumerator EnemyPatrolLeft()
    {
        Vector3 patrolEdgeLeft = patrolPoint.position;
        patrolEdgeLeft -= new Vector3(patrolRange, 0, 0);
        patrolEdgeLeft.y = 0;
        while (transform.position.x > patrolPoint.position.x - patrolRange)
        {
            transform.position -= Vector3.Lerp(Vector3.zero, patrolEdgeLeft, Time.fixedDeltaTime * patrolSpeed);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(EnemyPatrolRight());
    }
}
