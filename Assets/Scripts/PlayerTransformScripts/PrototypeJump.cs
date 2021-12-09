using System;
using System.Collections;
using UnityEngine;

public class PrototypeJump : MonoBehaviour
{
    [SerializeField] private AnimationCurve _YAnimation;

    [SerializeField] private float timeDuration = 1;
    [SerializeField] private float height = 1;

    private Rigidbody2D rb;
    private Coroutine _currentJump;

    public int _jumpCount = 2;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void PlayAnimation(Transform jumper, float duration)
    {
        if (_jumpCount == 2)
        {
            _jumpCount -= 1;
            _currentJump = StartCoroutine(AnimationPlaying(jumper, duration));
        }
        else if (_jumpCount == 1)
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

        rb.gravityScale = 0.9f;

        while (progress < 1)
        {
            expiredTime += Time.fixedDeltaTime * 0.1f;
            
            progress = expiredTime / jumpDuration;
            
            if (_jumpCount == 0) jumper.position += new Vector3(0, _YAnimation.Evaluate(progress) * (height * 1.3f), 0);
            else jumper.position += new Vector3(0, _YAnimation.Evaluate(progress) * height, 0);
            
            yield return null;
        }
        yield return new WaitForSeconds(0.05f);
        rb.gravityScale = 1;
    }
    public void PlayerJump()
    {
        PlayAnimation(transform, timeDuration);
    }
}
