using System.Collections;
using UnityEngine;

public class PrototypeJump : MonoBehaviour
{
    [SerializeField] private AnimationCurve yAnimation;

    [SerializeField] private float timeDuration = 1;
    [SerializeField] private float height = 1;

    private Rigidbody2D _rb;
    private Coroutine _currentJump;

    public Vector3 jumpPosition;

    public int jumpCount = 2;
    private static readonly int Jump = Animator.StringToHash("Jump");

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void PlayAnimation(Animator animator)
    {
        if (jumpCount == 2)
        {
            jumpCount -= 1;
            StartCoroutine(AnimationPlaying(animator));
        }
        else if (jumpCount == 1)
        {
            jumpCount -= 1;
            StopAllCoroutines();
            StartCoroutine(AnimationPlaying(animator));
        }
    }

    private IEnumerator AnimationPlaying(Animator animator)
    {
        animator.SetTrigger(Jump);
        float expiredTime = 0;
        float progress = 0;

        _rb.gravityScale = 0;

        while (progress < 1)
        {
            expiredTime += Time.deltaTime;
            progress = expiredTime / timeDuration;
            
            var endPos = new Vector3(0, yAnimation.Evaluate(progress) * height, 0);
            //jumpPosition = Vector3.Lerp(Vector3.zero, endPos, Time.deltaTime);
            jumpPosition = endPos;

            yield return null;
        }
        _rb.gravityScale = 1;
        jumpPosition = Vector3.zero;
    }
    public void PlayerJump(Animator animator)
    {
        PlayAnimation(animator);
    }
}
