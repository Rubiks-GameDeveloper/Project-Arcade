using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Prototype_PlayerMovementRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //���� ��� ���������, ����� ������, ������������ ��������� �� ������ ���� � ������(���������) )))))

    [SerializeField] private GameObject player;
    [SerializeField] private float speedRight;

    private bool isTapInButton = false;
    //��������, ����� �� ����� � ������� ������(������)
    public void OnPointerDown(PointerEventData eventData)
    {
        CameraTransform.isPlayerMove = true;
        isTapInButton = true;
        if (player.transform.rotation.y > 0) player.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
    }
    //��������, ����� �� ����� ����� � ������(������)
    public void OnPointerUp(PointerEventData eventData)
    {
        CameraTransform.isPlayerMove = false;
        isTapInButton = false;
    }
    private void FixedUpdate()
    {
        //����������� ������
        if (isTapInButton) player.transform.position += new Vector3(speedRight * Time.deltaTime, 0, 0);
    }
}
