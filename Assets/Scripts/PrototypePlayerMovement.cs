using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrototypePlayerMovement : MonoBehaviour
{
    public float speed;
    [SerializeField] private GameObject player;
    [SerializeField] private Joystick playerMovementJoystick;

    public static bool IsPlayerMove = false;
    private bool _isPlayerNotNull;

    void Start()
    {
        player = gameObject;
        _isPlayerNotNull = player != null;
    }

    private void FixedUpdate()
    {
        if (playerMovementJoystick.Horizontal != 0 && _isPlayerNotNull)
        {
            IsPlayerMove = true;

            player.transform.position += new Vector3(playerMovementJoystick.Horizontal * speed, 0, 0);
            if (playerMovementJoystick.Horizontal < 0)
                player.transform.rotation = Quaternion.AngleAxis(180f, Vector3.down);
            else if (playerMovementJoystick.Horizontal >= 0)
                player.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
        else
            IsPlayerMove = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(1);
    }
}
