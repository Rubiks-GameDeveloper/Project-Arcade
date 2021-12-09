using System;
using System.Collections;
using UnityEngine;

public class PlayerProgrammingTransformer : MonoBehaviour
{
    [SerializeField] private Joystick playerJoystick;
    [SerializeField] private PrototypeJump jump;
    [SerializeField] private VectorMovement playerMove;

    private bool _onGround;
    private bool _isJumped;

    [SerializeField] private Transform groundChecker;
    private Coroutine _dataGroundCheckCoroutine;

    private void PlayerMovement()
    {
        if (playerJoystick.Horizontal != 0)
        {
            playerMove.Move(new Vector2(playerJoystick.Horizontal, 0));
            if (playerJoystick.Horizontal < 0)
                gameObject.transform.rotation = Quaternion.AngleAxis(180f, Vector3.down);
            else if (playerJoystick.Horizontal >= 0)
                gameObject.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
    }
    public void Jump()
    {
        jump.PlayerJump(_onGround, _isJumped);
    } 
    private void GroundCheck()
        {
            Collider2D[] colliders = new Collider2D[25];
            var res = Physics2D.OverlapCircleNonAlloc(groundChecker.position, 0.85f, colliders);
        
            if (res > 1)
            {
                foreach (var item in colliders)
                {
                    if (item.gameObject.CompareTag("Ground"))
                    {
                        _onGround = true;
                        _isJumped = false;
                        return;
                    }
                }
            }
            if (_dataGroundCheckCoroutine == null)
            {
                _dataGroundCheckCoroutine = StartCoroutine(PlayerGroundCheck());
            }
        }
    private IEnumerator PlayerGroundCheck()
    {
        yield return new WaitForSeconds(0.1f);
        _onGround = false;
        _isJumped = true;
        _dataGroundCheckCoroutine = null;
        yield return new WaitForFixedUpdate();
    }

    private void FixedUpdate()
    {
        GroundCheck();
        PlayerMovement();
        if (Input.GetButtonDown("Jump")) Jump();
    }
}
