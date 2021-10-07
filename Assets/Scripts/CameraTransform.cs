using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public static bool isPlayerMove = false;

    private void Start()
    {
        GetComponent<Camera>().orthographicSize = 5;
    }

    private void FixedUpdate()
    {
        if (player.transform.position.y > -2f && isPlayerMove)
        {
            transform.position = new Vector3(player.transform.position.x, 0, -10);
        }
    }
}
