using UnityEngine;

public class PrototypePlayerMovement : MonoBehaviour
{
    [SerializeField] private VectorMovement playerMove;
    [SerializeField] private Joystick playerMovementJoystick;

    private void FixedUpdate()
    {
        if (playerMovementJoystick.Horizontal != 0)
        {
            playerMove.Move(new Vector2(playerMovementJoystick.Horizontal, 0));
            if (playerMovementJoystick.Horizontal < 0)
                gameObject.transform.rotation = Quaternion.AngleAxis(180f, Vector3.down);
            else if (playerMovementJoystick.Horizontal >= 0)
                gameObject.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
    }
}
