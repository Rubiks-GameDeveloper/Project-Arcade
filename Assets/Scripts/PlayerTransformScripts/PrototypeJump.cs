using System;
using System.Collections;
using UnityEngine;

public class PrototypeJump : MonoBehaviour
{
    [SerializeField] private AnimationCurve yAnimation;

    [SerializeField] private float timeDuration = 1;
    [SerializeField] private float height = 1;
    [SerializeField] [Range(1, 2)] private float upTimeCoefficient;

    private Rigidbody2D _rb;
    private Coroutine _currentJump;

    public Vector3 jumpPosition;

    public int jumpCount = 2;
    private static readonly int Jump = Animator.StringToHash("Jump");

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //print(_currentJump);
    }

    public void PlayAnimation(Animator animator)
    {
        switch (jumpCount)
        {
            case 2:
                print("Operation complete");
                jumpCount -= 1;
                _currentJump = StartCoroutine(AnimationPlaying(animator));
                break;
            case 1:
                jumpCount -= 1;
                StopAllCoroutines();
                _currentJump = StartCoroutine(AnimationPlaying(animator));
                break;
            case 0:
                break;
        }
    }

    private IEnumerator AnimationPlaying(Animator animator)
    {
        animator.SetTrigger(Jump);
        float expiredTime = 0;
        float progress = 0;

        //_rb.gravityScale = 0;

        while (progress < 1)
        {
            expiredTime += Time.deltaTime;
            progress = expiredTime / timeDuration * upTimeCoefficient;
            
            var endPos = new Vector3(0, yAnimation.Evaluate(progress) * height, 0);

            if (progress < 0.49f) jumpPosition = endPos;
            //else jumpPosition = -endPos;
            yield return null;
        }
        //_rb.gravityScale = 1;
        jumpPosition = Vector3.zero;
    }
    public void PlayerJump(Animator animator)
    {
        PlayAnimation(animator);
    }
}
