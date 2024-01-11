using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBtnManager : MonoBehaviour
{
    public GameObject[] onBtns;  //ä�� ����, �� ����, ä�� �ο�����, ��� ����
    public GameObject[] panels; //ä�� �г�, �� ���� �г�, ä�� �ο� �г�, ��� ���� �г�
    public GameObject[] titleInputs; //���� �Է�â, ���� �Է� ����â
    public GameObject outRoom; 

    bool isClosed = true;

    private void Start()
    {
        CloseAllPanel();
        MasterCheck();
        onBtns[0].SetActive(true);
    }

    public void OnChat()
    {
        for(int i = 0; i < onBtns.Length; i++)
        {
            onBtns[i].SetActive(true);
            panels[i].SetActive(true);
        }
        onBtns[0].SetActive(false);
        isClosed = false;
    }

    public void ListCick()
    {
        ClosePanel();
        panels[2].SetActive(true);
    }

    public void TeamClick()
    {
        ClosePanel();
        panels[1].SetActive(true);
    }

    public void WinnerClick()
    {
        ClosePanel();
        panels[3].SetActive(true);
    }
    
    public void ChatOff()
    {
        CloseAllPanel();
        panels[0].SetActive(true);
        isClosed = true;
    }

    public void ClosePanel()
    {
        for (int i = 1; i < onBtns.Length; i++)
        {
            panels[i].SetActive(false);
        }
    }

    void CloseAllPanel()
    {
        for (int i = 0; i < onBtns.Length; i++)
        {
            onBtns[i].SetActive(false);
            panels[i].SetActive(false);
        }
    }

    public void MasterCheck()
    {
        if (ServerManager.instance.CheckMaster())
        {
            titleInputs[0].SetActive(true);
            titleInputs[1].SetActive(true);
            if(!isClosed)
                onBtns[3].SetActive(true);
        }
        else
        {
            titleInputs[0].SetActive(false);
            titleInputs[1].SetActive(false);
            onBtns[3].SetActive(false);
        }
    }

    public void RoomOutClick()
    {
        outRoom.SetActive(true);

    }

    public void YesClick()
    {
        outRoom.SetActive(false);
        ServerManager.instance.LeaveRoom();
    }

    public void NoClick()
    {
        outRoom.SetActive(false);
    }

    private void Update()
    {
        MasterCheck();
    }
}
