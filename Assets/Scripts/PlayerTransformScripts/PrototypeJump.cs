using System;
using System.Collections;
using UnityEngine;

public class PrototypeJump : MonoBehaviour
{
    [SerializeField] private AnimationCurve _YAnimation;

    [SerializeField] private float timeDuration = 1;
    [SerializeField] private float height = 1;

    private Rigidbody2D _rb;
    private Coroutine _currentJump;

    public int jumpCount = 2;
    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void PlayAnimation(Transform jumper, float duration)
    {
        
        if (jumpCount == 2)
        {
            jumpCount -= 1;
            _currentJump = StartCoroutine(AnimationPlaying(jumper, duration));
        }
        else if (jumpCount == 1)
        {
            jumpCount -= 1;
            StopCoroutine(_currentJump);
            StartCoroutine(AnimationPlaying(jumper, duration));
        }
    }
    private IEnumerator AnimationPlaying(Transform jumper, float jumpDuration)
    {
        float expiredTime = 0;
        float progress = 0;

        _rb.gravityScale = 0.9f;

        while (progress < 1)
        {
            expiredTime += Time.fixedDeltaTime * 0.1f;
            
            progress = expiredTime / jumpDuration;
            
            if (jumpCount == 0) jumper.position += new Vector3(0, _YAnimation.Evaluate(progress) * (height * 1.3f), 0);
            else jumper.position += new Vector3(0, _YAnimation.Evaluate(progress) * height, 0);
            
            yield return null;
        }
        yield return new WaitForSeconds(0.05f);
        _rb.gravityScale = 1;
    }
    public void PlayerJump(Animator animator)
    {
        animator.SetTrigger("Jump");
        PlayAnimation(transform, timeDuration);
    }
}
