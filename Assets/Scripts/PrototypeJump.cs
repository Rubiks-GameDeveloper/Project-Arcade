using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeJump : MonoBehaviour
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
        float expiredTime = 0;
        float progress = 0;

        Vector3 startPosition = jumper.position; 

        while (progress < 1)
        {
            expiredTime += Time.fixedDeltaTime * 0.05f;
            
            progress = expiredTime / duration;
            
            jumper.position += new Vector3(0, _YAnimation.Evaluate(progress) * _height, 0);

            yield return null;
        }
    }
    
    public void PlayerJump()
    {
        PlayAnimation(transform, _duration);
    }
}
