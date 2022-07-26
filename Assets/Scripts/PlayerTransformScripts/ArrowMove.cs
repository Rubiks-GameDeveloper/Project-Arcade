using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowMove : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private PlayerProgrammingTransformer transformer;
    [SerializeField] private float xMoveVector;
    private static bool _isTouchInArrows;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isTouchInArrows) transformer.xMoveVector = xMoveVector;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transformer.xMoveVector = 0;
        _isTouchInArrows = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transformer.xMoveVector = 0;
        _isTouchInArrows = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isTouchInArrows = true;
        transformer.xMoveVector = xMoveVector;
    }
}
