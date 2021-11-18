using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prototype_Jump : MonoBehaviour
{
    [SerializeField] private AnimationCurve _YAnimation;

    [SerializeField] private float _duration = 1;
    [SerializeField] private float _height = 1;
    public void PlayAnimation(Transform jumper, float duration)
    {
        StartCoroutine(AnimationPlaying(jumper, duration));
    }

    private IEnumerator AnimationPlaying(Transform jumper, float duration)
    {
        float expiriedTime = 0;
        float progress = 0;

        Vector3 startPosition = jumper.position; 

        while (progress < 1)
        {
            expiriedTime += Time.fixedDeltaTime * 0.05f;
            
            progress = expiriedTime / duration;
            
            jumper.position += new Vector3(0, _YAnimation.Evaluate(progress) * _height, 0);

            yield return null;
        }
    }
    public void PlayerJump()
    {
        PlayAnimation(transform, _duration);
    }
}
