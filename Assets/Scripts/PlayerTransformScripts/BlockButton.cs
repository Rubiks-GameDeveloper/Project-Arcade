using UnityEngine;
using UnityEngine.EventSystems;
using FightSystem;

public class BlockButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private ProgrammingPlayerFightSystem playerFightSystem;
    public void OnPointerDown(PointerEventData data)
    {
        playerFightSystem.PlayerBlock();
    }

    public void OnPointerUp(PointerEventData data)
    {
        playerFightSystem.PlayerBlock();
    }
}
