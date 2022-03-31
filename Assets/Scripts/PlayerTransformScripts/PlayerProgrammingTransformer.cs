using System.Collections;
using FightSystem;
using UIScripts;
using UnityEngine;

public class PlayerProgrammingTransformer : MonoBehaviour
{
    public Joystick playerJoystick;
    [SerializeField] private PrototypeJump jump;
    [SerializeField] private VectorMovement playerMove;
    
    //private bool _onGround;
    [SerializeField] private float groundCheckerRange;
    [SerializeField] private float playerSpeed;
    
    [SerializeField] private Transform groundChecker;
    private Animator _playerAnimator;
    private ProgrammingPlayerFightSystem _playerFightSystem;
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int Run = Animator.StringToHash("Run");

    private void Start()
    {
        Application.targetFrameRate = OptionMenu.TargetFrameRate;
        _playerAnimator = GetComponent<Animator>();
        _playerFightSystem = GetComponent<ProgrammingPlayerFightSystem>();
    }

    private void PlayerMovement()
    {
        var playerCanMove = !_playerFightSystem.isPlayerAttack && !_playerFightSystem.isPlayerStun &&
                            !_playerFightSystem.isPlayerBlock;
        if (playerJoystick.Horizontal != 0 && playerCanMove)
        {
            _playerAnimator.SetBool(Run, true);
            playerMove.Move(new Vector2(playerJoystick.Horizontal, 0), playerSpeed, jump.jumpPosition);
            if (playerJoystick.Horizontal < 0)
                gameObject.transform.rotation = Quaternion.AngleAxis(180f, Vector3.down);
            else if (playerJoystick.Horizontal >= 0)
                gameObject.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
        else if (playerCanMove && jump.jumpPosition != Vector3.zero) playerMove.Move(Vector3.zero, 0, jump.jumpPosition);
        else _playerAnimator.SetBool(Run, false);
    }
    public void Jump()
    {
        if (!_playerFightSystem.isPlayerStun || !_playerFightSystem.isPlayerAttack || !_playerFightSystem.isPlayerBlock)
        {
            jump.PlayerJump(_playerAnimator);
        }
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
                if (item != null && item.gameObject.CompareTag("Ground"))
                {
                    
                    _playerAnimator.SetBool(Grounded, true);
                    return;
                }
            }
        }
        _playerAnimator.SetBool(Grounded, false);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && other.transform.position.y < transform.position.y)
        {
            StopAllCoroutines();
            StartCoroutine(JumpCountReceive());
            _playerAnimator.SetBool(Grounded, true);
        }
        else if (other.gameObject.CompareTag("Ground") && other.transform.position.y >= transform.position.y)
        {
            jump.StopAllCoroutines();
            jump.jumpPosition = Vector3.zero;
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
    private IEnumerator JumpCountReceive()
    {
        yield return new WaitForSeconds(0.15f);
        jump.jumpCount = 2;
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
