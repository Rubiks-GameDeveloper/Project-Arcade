using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPrototype : MonoBehaviour
{
    private GameObject enemyData;
    //Проверка триггера есть ли в нём враг
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Если есть, запоминаем его
        if (collision.gameObject.tag == "Enemy")
        {
            enemyData = collision.gameObject;
        } 
    }

    private void FixedUpdate()
    {
        //Опять же, так не надо писать!!! Проверяем, нажал ли игрок левую кнопку мыши, если нажал убираем врага
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(enemyData);
            enemyData = null;
        }
    }
}
