using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            return;
        }
    }

    [HideInInspector] public string myColor = "FFFFFF";  //�÷��̾� �̸� ����
    [HideInInspector] public string myName = "";  //�÷��̾� �̸�
    [HideInInspector] public bool isGuest = false;   //�÷��̾��� �Խ�Ʈ��� ����
    [HideInInspector] public string myGhostColor = "";  //���� ����

    public void GuestClick()
    {
        isGuest = true;
        LoginSuccess();
    }

    public void LoginSuccess()  //�α��� ������ �κ� �̵�
    {
        if (isGuest)
            myName = "Guest240" + Random.Range(1, 101);
        ServerManager.instance.GotoLobby();
    }

    public void ExitGame() //���� ���� + ���� ���� ����
    {
        ServerManager.instance.OnDisconnect();
        Application.Quit();
    }

    private void FixedUpdate()
    {
        
    }
}
