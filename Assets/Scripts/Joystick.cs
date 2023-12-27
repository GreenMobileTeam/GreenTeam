using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform rect_JoystickBG;
    [SerializeField] private RectTransform rect_Joystick;
    [SerializeField] private float moveSpeed;

    private RespawnGhost respawnGhost; // ������ ��Ʈ �ҷ���
    private float joystick_Radius; // rect_JoystickBG�� ������
    private bool isTouch = false;
    private Vector3 mVector;

    void Start()
    {
        joystick_Radius = rect_JoystickBG.rect.width * 0.5f;
        if (respawnGhost == null)
        {
            respawnGhost = FindObjectOfType<RespawnGhost>();
        }
    }

    void Update()
    {
        if (isTouch)
        {
            respawnGhost.playerGhost.transform.position += mVector;
            respawnGhost.playerGhost.transform.LookAt(respawnGhost.playerGhost.transform.position + mVector);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ���콺 �������� x, y�� �ۿ� ��� Vector2 ������ ����ȯ
        Vector2 dragValue = eventData.position - (Vector2)rect_JoystickBG.position;

        // ClampMagnitude - ���̽�ƽ ��������ŭ ���α�
        dragValue = Vector2.ClampMagnitude(dragValue, joystick_Radius);

        // �θ� ��ü �������� ������� ��ǥ
        rect_Joystick.localPosition = dragValue;

        // �Ÿ��� ���ϱ�, �߽����� ������ �÷��̾� �ӵ� �پ��
        float joystick_distance = Vector2.Distance(rect_JoystickBG.position, rect_Joystick.position) / joystick_Radius;

        // �÷��̾� �����̴� ����
        dragValue = dragValue.normalized;
        mVector = new Vector3(dragValue.x * moveSpeed * joystick_distance * Time.deltaTime, 0f, dragValue.y * moveSpeed * joystick_distance * Time.deltaTime);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false;
        rect_Joystick.localPosition = new Vector3(0, 0, 0);
        mVector = Vector3.zero;
    }

}
