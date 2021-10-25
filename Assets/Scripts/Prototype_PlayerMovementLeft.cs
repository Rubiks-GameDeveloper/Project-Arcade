using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Prototype_PlayerMovementLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //���� ��� ���������, ����� ������, ������������ ��������� �� ������ ���� � ������(���������) )))))

    [SerializeField] private GameObject player;
    [SerializeField] private float speedLeft;

    private bool isTapInButton = false;
    //��������, ����� �� ����� � ������� ������(�����)
    public void OnPointerDown(PointerEventData eventData)
    {
        CameraTransform.isPlayerMove = true;
        isTapInButton = true;
        if (player.transform.rotation.y <= 0) player.transform.rotation = Quaternion.AngleAxis(180f, Vector3.down);
    }
    //��������, ����� �� ����� ����� � ������(�����)
    public void OnPointerUp(PointerEventData eventData)
    {
        CameraTransform.isPlayerMove = false;
        isTapInButton = false;
    }
    private void FixedUpdate()
    {
        //����������� �����
        if (isTapInButton) player.transform.position -= new Vector3(speedLeft * Time.deltaTime, 0, 0);
    }
}
