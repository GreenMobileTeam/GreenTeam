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

    [HideInInspector] public string myColor = "FFFFFF";  //플레이어 이름 색상
    [HideInInspector] public string myName = "";  //플레이어 이름
    [HideInInspector] public bool isGuest = false;   //플레이어의 게스트모드 유무
    [HideInInspector] public string myGhostColor = "";  //유령 색상

    public void GuestClick()
    {
        isGuest = true;
        LoginSuccess();
    }

    public void LoginSuccess()  //로그인 성공시 로비 이동
    {
        if (isGuest)
            myName = "Guest240" + Random.Range(1, 101);
        ServerManager.instance.GotoLobby();
    }

    public void ExitGame() //게임 종료 + 포톤 접속 종료
    {
        ServerManager.instance.OnDisconnect();
        Application.Quit();
    }

    private void FixedUpdate()
    {
        
    }
}
