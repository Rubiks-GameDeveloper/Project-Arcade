using System;
using System.Collections;
using UnityEngine;

public class PrototypeJump : MonoBehaviour
{
    [SerializeField] private AnimationCurve _YAnimation;

    [SerializeField] private float timeDuration = 1;
    [SerializeField] private float height = 1;

    private Rigidbody2D rb;
    private int _jumpCount = 2;
    private Coroutine _currentJump;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void PlayAnimation(Transform jumper, float duration, bool _onGround, bool _isJumped)
    {
        if (_onGround)
        {
            _jumpCount -= 1;
            _currentJump = StartCoroutine(AnimationPlaying(jumper, duration));
        }
        else if (_isJumped && _jumpCount > 0)
        {
            _jumpCount -= 1;
            StopCoroutine(_currentJump);
            StartCoroutine(AnimationPlaying(jumper, duration));
        }
    }
    private IEnumerator AnimationPlaying(Transform jumper, float jumpDuration)
    {
        float expiredTime = 0;
        float progress = 0;

        rb.gravityScale = 0.75f;

        while (progress < 1)
        {
            expiredTime += Time.fixedDeltaTime * 0.1f;
            
            progress = expiredTime / jumpDuration;
            
            if (_jumpCount == 0) jumper.position += new Vector3(0, _YAnimation.Evaluate(progress) * (height * 1.3f), 0);
            else jumper.position += new Vector3(0, _YAnimation.Evaluate(progress) * height, 0);
            
            yield return null;
        }
        yield return new WaitForSeconds(0.15f);
        rb.gravityScale = 1;
    }
    public void PlayerJump(bool _onGround, bool _isJumped)
    {
        if (_onGround && !_isJumped) _jumpCount = 2;
        PlayAnimation(transform, timeDuration, _onGround, _isJumped);
    }
}
