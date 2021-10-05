using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPrototype : MonoBehaviour
{
    private GameObject enemyData;
    //�������� �������� ���� �� � �� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���� ����, ���������� ���
        if (collision.gameObject.tag == "Enemy")
        {
            enemyData = collision.gameObject;
        } 
    }

    private void FixedUpdate()
    {
        //����� ��, ��� �� ���� ������!!! ���������, ����� �� ����� ����� ������ ����, ���� ����� ������� �����
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(enemyData);
            enemyData = null;
        }
    }
}
