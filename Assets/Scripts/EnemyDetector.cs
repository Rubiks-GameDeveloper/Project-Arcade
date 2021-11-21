using System;
using TMPro;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    private Vector3 _playerPosition;
    private Vector3 _enemyPosition;

    private void Start()
    {
        _enemyPosition = enemy.transform.position;
    }

    private void FixedUpdate()
    {
        _playerPosition = gameObject.transform.position;
        double distanceToEnemy = Math.Sqrt(Math.Pow(_enemyPosition.x - _playerPosition.x, 2) +
                                           Math.Pow(_enemyPosition.y - _playerPosition.y, 2));
        if (distanceToEnemy < 2) enemy.SetActive(false);
        else enemy.SetActive(true);
    }
}
