using System.Collections;
using UnityEngine;

public class PrototypeJump : MonoBehaviour
{
    [SerializeField] private AnimationCurve _YAnimation;

    [SerializeField] private float duration = 1;
    [SerializeField] private float height = 1;

    [SerializeField] private Transform groundChecker;

    private bool _onGround;
    private bool _jumped;

    private Coroutine _currentJump;
    
    private int _jumpCount = 2;
    private void PlayAnimation(Transform jumper, float duration)
    {
        if (_onGround)
        {
            _jumpCount -= 1;
            _currentJump = StartCoroutine(AnimationPlaying(jumper, duration));
        }
        else if (_jumped && _jumpCount > 0)
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

        while (progress < 1)
        {
            expiredTime += Time.fixedDeltaTime * 0.1f;
            
            progress = expiredTime / jumpDuration;
            
            jumper.position += new Vector3(0, _YAnimation.Evaluate(progress) * height, 0);

            yield return null;
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        Collider2D[] colliders = new Collider2D[25];
        var res = Physics2D.OverlapCircleNonAlloc(groundChecker.position, 0.6f, colliders);
 
        if (res > 1)
        {
            foreach (var item in colliders)
            {
                if (item.gameObject.CompareTag("Ground"))
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
        PlayAnimation(transform, duration);
    }
}
