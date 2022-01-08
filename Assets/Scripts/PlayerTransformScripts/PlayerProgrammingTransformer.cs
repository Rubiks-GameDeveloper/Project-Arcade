using System;
using System.Collections;
using UnityEngine;

public class PlayerProgrammingTransformer : MonoBehaviour
{
    [SerializeField] private Joystick playerJoystick;
    [SerializeField] private PrototypeJump jump;
    [SerializeField] private VectorMovement playerMove;
    
    //private bool _onGround;
    [SerializeField] private float groundCheckerRange;
    
    [SerializeField] private Transform groundChecker;
    private Animator _playerAnimator;

    [SerializeField] private VectorValue pos; //A

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();

        transform.position = pos.initialValue;
    }

    private void PlayerMovement()
    {
        if (playerJoystick.Horizontal != 0)
        {
            _playerAnimator.SetBool("Run", true);
            playerMove.Move(new Vector2(playerJoystick.Horizontal, 0));
            if (playerJoystick.Horizontal < 0)
                gameObject.transform.rotation = Quaternion.AngleAxis(180f, Vector3.down);
            else if (playerJoystick.Horizontal >= 0)
                gameObject.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
        else _playerAnimator.SetBool("Run", false);
    }
    public void Jump()
    {
        jump.PlayerJump(_playerAnimator);
    }
    private void GroundChecker()
    {
        Collider2D[] colliders = new Collider2D[25];
        var size = new Vector2(groundCheckerRange, 0.2f);
        var res = Physics2D.OverlapBoxNonAlloc(groundChecker.position, size, 0, colliders);
        
        if (res > 1)
        {
            foreach (var item in colliders)
            {
                if (item.gameObject.CompareTag("Ground") && item != null)
                {
                    _playerAnimator.SetBool("Grounded", true);
                    return;
                }
            }
        }
        _playerAnimator.SetBool("Grounded", false);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jump.jumpCount = 2;
            _playerAnimator.SetBool("Grounded", true);
            //_onGround = true;
        }
    }
    private void FixedUpdate()
    {
        GroundChecker();
        PlayerMovement();
        if (Input.GetButtonDown("Jump")) Jump();
    }

    private void OnDrawGizmos()
    {
        var size = new Vector2(groundCheckerRange, 0.2f);
        Gizmos.DrawWireCube(groundChecker.position, size);
    }
}
