using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform rect_JoystickBG;
    [SerializeField] private RectTransform rect_Joystick;
    [SerializeField] private float moveSpeed;

    private RespawnGhost respawnGhost; // 생성된 고스트 불러옴
    private float joystick_Radius; // rect_JoystickBG의 반지름
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
        // 마우스 포지션은 x, y축 밖에 없어서 Vector2 강제로 형변환
        Vector2 dragValue = eventData.position - (Vector2)rect_JoystickBG.position;

        // ClampMagnitude - 조이스틱 반지름만큼 가두기
        dragValue = Vector2.ClampMagnitude(dragValue, joystick_Radius);

        // 부모 객체 기준으로 상대적인 좌표
        rect_Joystick.localPosition = dragValue;

        // 거리차 구하기, 중심으로 갈수록 플레이어 속도 줄어듬
        float joystick_distance = Vector2.Distance(rect_JoystickBG.position, rect_Joystick.position) / joystick_Radius;

        // 플레이어 움직이는 방향
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
