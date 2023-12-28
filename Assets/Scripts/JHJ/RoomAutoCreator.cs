using UnityEngine;
using TMPro;

public class RoomAutoCreator : MonoBehaviour
{
    [Header("방의 개수")]
    [Tooltip("방의 개수를 입력하는 곳")]
    public int rooms;
    [Header("기타 오브젝트")]
    public GameObject roomBtn;
    public Transform btnPoz;

    private void Start()
    {
        for(int i = 1; i <= rooms; i++)
        {
            GameObject pref = Instantiate(roomBtn);
            pref.name = "Room" + i;
            TextMeshProUGUI text = pref.GetComponentInChildren<TextMeshProUGUI>();
            text.text = i + "번 채팅방";
            pref.transform.SetParent(btnPoz);
        }
    }
}
