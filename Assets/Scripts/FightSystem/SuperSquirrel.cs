using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SuperSquirrel : MonoBehaviour
{
    private Enemy _sSqirrel;
    [SerializeField] private Transform reactionPoint;
    [SerializeField] private float reactionRange;
    [SerializeField] private LayerMask playerLayerMask;

    private Transform player;
    private void Start()
    {
        _sSqirrel = GetComponent<Enemy>();
        StartCoroutine(_sSqirrel.EnemyPatrolRight());
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Collider2D playerInRange = Physics2D.OverlapCircle(reactionPoint.position, reactionRange, playerLayerMask.value);
        if (playerInRange != null)
        {
            var distance = _sSqirrel.playerDistanceToEnemy(transform, playerInRange.transform);
            _sSqirrel.EnemyReacting(distance, reactionRange, playerInRange, player);
        }
        else
        {
            _sSqirrel.EnemyReacting(reactionRange + 2f, reactionRange, playerInRange, player);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(reactionPoint.position, reactionRange);
    }
}
