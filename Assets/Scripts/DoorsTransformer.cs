using UnityEngine;
using UnityEngine.Events;
public class DoorsTransformer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private UnityEvent doorActivate;
    private void OnTriggerEnter2D(Collider2D other)
    {
        doorActivate.Invoke();
    }

    private void DoorTeleportation (Transform exit)
    {
        player.transform.position = exit.position;
    }
}
