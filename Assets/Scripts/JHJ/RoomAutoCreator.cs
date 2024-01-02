using UnityEngine;
using TMPro;

public class RoomAutoCreator : MonoBehaviour
{
    [Header("���� ����")]
    [Tooltip("���� ������ �Է��ϴ� ��")]
    public int rooms;
    [Header("��Ÿ ������Ʈ")]
    public GameObject roomBtn;
    public Transform btnPoz;

    private void Start()
    {
        for(int i = 1; i <= rooms; i++)
        {
            GameObject pref = Instantiate(roomBtn);
            pref.name = "Room" + i;
            TextMeshProUGUI text = pref.GetComponentInChildren<TextMeshProUGUI>();
            text.text = i + "�� ä�ù�";
            pref.transform.SetParent(btnPoz);
        }
    }
}
