using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeJump : MonoBehaviour
{
    [SerializeField] private AnimationCurve _YAnimation;

    [SerializeField] private float _duration = 1;
    [SerializeField] private float _height = 1;

    [SerializeField] private Transform groundChecker;

    private bool _onGround;
    private bool _jumped;

    private int _jumpCount = 2;
    public void PlayAnimation(Transform jumper, float duration)
    {
        if (_onGround)
        {
            _jumpCount -= 1;
            StartCoroutine(AnimationPlaying(jumper, duration));
        }
        else if (_jumped && _jumpCount > 0)
        {
            _jumpCount -= 1;
            StartCoroutine(AnimationPlaying(jumper, duration));
        }
    }

    private IEnumerator AnimationPlaying(Transform jumper, float duration)
    {
        float expiredTime = 0;
        float progress = 0;

        Vector3 startPosition = jumper.position; 

        while (progress < 1)
        {
            expiredTime += Time.fixedDeltaTime * 0.1f;
            
            progress = expiredTime / duration;
            
            jumper.position += new Vector3(0, _YAnimation.Evaluate(progress) * _height, 0);

            yield return null;
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();
        //if (_onGround && !_jumped) _jumpCount = 2;
    }

    private void GroundCheck()
    {
        Collider2D[] _colliders = new Collider2D[25];
        var res = Physics2D.OverlapCircleNonAlloc(groundChecker.position, 0.6f, _colliders);
 
        if (res > 1)
        {
            foreach (var item in _colliders)
            {
                if (item != null && item.gameObject.CompareTag("Ground"))
                {
                    _onGround = true;
                    _jumped = false;
                    return;
                }
            }
        }
        _onGround = false;
        _jumped = true;
    }
    
    public void PlayerJump()
    {
        if (_onGround && !_jumped) _jumpCount = 2;
        PlayAnimation(transform, _duration);
    }
}
