using System;
using System.Collections;
using System.Linq;
using FightSystem;
using ForFunOnLevels;
using UIScripts;
using UnityEngine;

public class PlayerProgrammingTransformer : MonoBehaviour
{
    public Joystick playerJoystick;
    [SerializeField] private PrototypeJump jump;
    [SerializeField] private VectorMovement playerMove;

    public RotateClass RotateClass;
    
    //private bool _onGround;
    [SerializeField] private float groundCheckerRange;
    [SerializeField] private float playerSpeed;
    [SerializeField] [Range(1f, 25f)] private float playerFallSpeed;
    
    [SerializeField] private Transform groundChecker;
    private Animator _playerAnimator;
    private ProgrammingPlayerFightSystem _playerFightSystem;
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int Run = Animator.StringToHash("Run");

    [SerializeField] private ParticleSystem dirtParticles;
    public GameObject ladderButton;
    
    public float xMoveVector;

    public bool isGravityActive = false;

    public GameObject currentGround;

    private Rigidbody2D _rb;
    private GameObject _ladder;
    public GameObject Ladder
    {
        get => _ladder;
        set
        {
            if (value.CompareTag("Ladder"))
            {
                _ladder = value;
            }
        }
    }

    public void LadderUse()
    {
        if(_ladder != null) _ladder.GetComponent<Ladder>().PlayerLadderTeleportation(gameObject);
    }

    private void Start()
    {
        dirtParticles.Pause();
        RotateClass = new RotateClass(gameObject);
        Application.targetFrameRate = OptionMenu.TargetFrameRate;
        ladderButton.SetActive(false);
        _playerAnimator = GetComponent<Animator>();
        _playerFightSystem = GetComponent<ProgrammingPlayerFightSystem>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void PlayerMovement()
    {
        var playerCanMove = /*!_playerFightSystem.isPlayerAttack &&*/ !_playerFightSystem.isPlayerStun &&
                            !_playerFightSystem.isPlayerBlock;
        if(isGravityActive || dirtParticles.isPlaying && playerJoystick.Horizontal == 0)
        {
            //dirtParticles.
            //if(!dirtParticles.isEmitting) dirtParticles.gameObject.SetActive(false);
        }
        if (playerJoystick.Horizontal != 0 && playerCanMove)
        {
            _playerAnimator.SetBool(Run, true);
            if (!isGravityActive && !dirtParticles.isPlaying)
            {
                dirtParticles.Simulate(Time.fixedDeltaTime * 20);
                //dirtParticles.gameObject.SetActive(true);
            }
            playerMove.Move(new Vector2(playerJoystick.Horizontal, 0), playerSpeed, jump.jumpPosition);
            if (playerJoystick.Horizontal < 0)
                RotateClass.TurnRight();
            else if (playerJoystick.Horizontal >= 0)
                RotateClass.TurnLeft();
        }
        else if (playerCanMove && jump.jumpPosition != Vector3.zero) playerMove.Move(Vector3.zero, 0, jump.jumpPosition);
        else
        {
            _playerAnimator.SetBool(Run, false);
        }
    }
    public void Jump()
    {
        if ((!_playerFightSystem.isPlayerStun || !_playerFightSystem.isPlayerBlock) && jump.jumpCount != 0)
        {
            isGravityActive = false;
            jump.PlayAnimation(_playerAnimator);
        }
    }
    private void GroundChecker()
    {
        var colliders = new Collider2D[25];
        var size = new Vector2(groundCheckerRange, 0.25f);
        var res = Physics2D.OverlapBoxNonAlloc(groundChecker.position, size, 0, colliders);
        if (res > 1)
        {
            if (colliders.Any(item => item != null && item.gameObject.CompareTag("Ground")))
            {
                currentGround = colliders.ToList().Find(item=> item != null && item.gameObject.CompareTag("Ground")).gameObject;
                //isGravityActive = false; 
                if (_rb.velocity.y == 0) jump.jumpCount = 2;
                _playerAnimator.SetBool(Grounded, true);
                return;
            }
        }

        //if (jump.jumpPosition == Vector3.zero) isGravityActive = true;
        _playerAnimator.SetBool(Grounded, false);
        currentGround = null;
    }
    private void JumpStopper()
    {
        //jump.StopAllCoroutines();
        jump.jumpPosition = Vector3.zero;
        isGravityActive = true;
    }
    /*private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.contactCount < 1) return;
        for (var a = 0; a < other.contactCount; a++)
        {
            if (!other.gameObject.CompareTag("Ground") ||
                !(other.contacts[a].point.x < gameObject.transform.position.x + 0.41f) ||
                !(other.contacts[a].point.x > gameObject.transform.position.x - 0.41f)) continue;
            if (other.contacts[a].point.y > gameObject.transform.position.y + 0.5f) JumpStopper();
            if (!(other.contacts[a].point.y < gameObject.transform.position.y + 0.2f)) continue;

            //_playerAnimator.SetBool(Grounded, true);
        }
    }*/
    /*private void OnCollisionExit2D(Collision2D other)
    {
        if (other.contactCount == 0)
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator JumpCountReceive()
    {
        yield return new WaitForSeconds(0.15f);
        if (jump.jumpCount < 2) jump.jumpCount = 2;
    }*/

    private void FixedUpdate()
    {
        //ArtificialGravity();
        GroundChecker();
        PlayerMovement();
        //if (Input.GetButton("Jump")) Jump();
    }

    private void ArtificialGravity()
    {
        if (isGravityActive)
        {
            playerMove.Move(Vector3.down, playerFallSpeed);
            
        }
    }

    private void OnDrawGizmos()
    {
        var size = new Vector2(groundCheckerRange, 0.25f);
        Gizmos.DrawWireCube(groundChecker.position, size);
    }
}
