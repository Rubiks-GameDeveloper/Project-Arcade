using System;
using System.Collections;
using UnityEngine;

public class PlayerProgrammingTransformer : MonoBehaviour
{
    [SerializeField] private Joystick playerJoystick;
    [SerializeField] private PrototypeJump jump;
    [SerializeField] private VectorMovement playerMove;

    private bool _onGround;

    [SerializeField] private Transform groundChecker;

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
        jump.PlayerJump();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jump._jumpCount = 2;
            _onGround = true;
        }
        else
        {
            _onGround = false;
        }
    }
    private void FixedUpdate()
    {
        PlayerMovement();
        if (Input.GetButtonDown("Jump")) Jump();
    }
}
