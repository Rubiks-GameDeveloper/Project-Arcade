using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prototype_Jump : MonoBehaviour
{
    //Вообще почти всё становится понятно из переменных, поэтому дерзай)
    [SerializeField] private AnimationCurve _YAnimation;

    [SerializeField] private float _duration = 1;
    [SerializeField] private float _height = 1;
    public void PlayAnimation(Transform jumper, float duration)
    {
        //Запускаем корутину (советую прочитать об этом официальную документацию, очень полезная вещь)
        StartCoroutine(AnimationPlaying(jumper, duration));
    }

    private IEnumerator AnimationPlaying(Transform jumper, float duration)
    {
        float expiriedTime = 0;
        float progress = 0;

        Vector3 startPosition = jumper.position; 

        while (progress < 1)
        {
            expiriedTime += Time.deltaTime;

            //Изменение прогресса прыжка
            progress = expiriedTime / duration;

<<<<<<< HEAD
            //Изменяем позицию (прибавляем) по оси Y (просто наводись на непонятные методы и читай)
=======
            //Изменяем позицию (прибавляем) по оси Y
>>>>>>> 9697546c9723f362b118cf4a7ae7643fc545175d
            jumper.position += new Vector3(0, _YAnimation.Evaluate(progress) * _height, 0);

            yield return null;
        }
    }
    private void Update()
    {
        //Проверяем нажатие кнопки
        if (Input.GetButtonUp("Jump"))
        {
            //Если нажата, то выполняем метод
            PlayAnimation(transform, _duration);
        }
    }
}
