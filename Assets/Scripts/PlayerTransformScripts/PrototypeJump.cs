using System;
using System.Collections;
using UnityEngine;

public class PrototypeJump : MonoBehaviour
{
    [SerializeField] private AnimationCurve yAnimation;
    public float jumpForce;
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
    public void PlayAnimation(Animator animator)
    {
        if (jumpCount != 2)
        {
            switch (jumpCount)
            {
                case 1:
                    jumpCount -= 1;
                    PhysicJump(animator);
                    break;
                case 0:
                    break;
            }
        }
        else
        {
            switch (jumpCount)
            {
                case 2:
                    jumpCount -= 1;
                    PhysicJump(animator);
                    break;
                case 1:
                    jumpCount -= 1;
                    _rb.velocity = Vector2.zero;
                    PhysicJump(animator);
                    break;
                case 0:
                    break;
            }
        }
        
    }
    private void PhysicJump(Animator animator)
    {
        animator.SetTrigger(Jump);
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
