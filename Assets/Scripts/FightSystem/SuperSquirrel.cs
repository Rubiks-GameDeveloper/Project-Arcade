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
        StartCoroutine(sSqirrel.EnemyPatrolRight(patrolPoint, patrolRange, patrolSpeed));
    }
    
    
}
