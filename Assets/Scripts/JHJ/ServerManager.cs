using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class ServerManager : MonoBehaviourPunCallbacks
{
    public static ServerManager instance;
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

    public GameObject noticePanel;
    public TextMeshProUGUI noticeTxt;
    [HideInInspector] public GameObject myGhost;

    string myName_;
    string roomName_;

    private void Start()
    {
        
    }

    void CreateRoom(string rName)
    {
        PhotonNetwork.CreateRoom(rName, new RoomOptions { MaxPlayers = 10 });
    }

    public void OnConnect() //로그인 전 포톤 서버 입장 확인
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnDisconnect()   //접속 종료
    {
        PhotonNetwork.Disconnect();
    }

    public void OnLeaveLobby()  //로비에서 나감(방 입장, 로그인 화면으로 재접속)
    {
        PhotonNetwork.LeaveLobby();
    }

    public override void OnLeftLobby()
    {
        base.OnLeftLobby();
        StopAllCoroutines();
        StartCoroutine(Notice("로비를 떠났습니다.", 1f));
    }

    public override void OnJoinedLobby() //로비 진입
    {
        StopAllCoroutines();
        StartCoroutine(Notice("로비 진입 완료!", 1f));
        base.OnJoinedLobby();
        PhotonNetwork.LoadLevel("Lobby");
        Debug.Log("로비 진입");

        for (int i = 1; i < 3; i++)
            CreateRoom("Map_" + i);
    }

    public override void OnConnectedToMaster()  // 마스터 서버 연결
    {
        base.OnConnectedToMaster();
        StopAllCoroutines();
        StartCoroutine(Notice("서버 접속 성공", 2f));
        Debug.Log("마스터 서버 접속");
    }

    public void GotoLobby() //로비 진입 시도
    {
        SceneManager.LoadScene("Lobby");
        StopAllCoroutines();
        StartCoroutine(Notice("로비 진입중...", 3f));

        OnConnect();
        myName_ = GameManager.instance.myName;
        PhotonNetwork.NickName = myName_;
    }

    public void onDisconnect() //아예 나감(다시 로그인 화면)
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        StartCoroutine(Notice("접속이 끊겼습니다", 1.2f));
    }

    IEnumerator Notice(string text, float time)
    {
        //Debug.Log("start");
        noticePanel.SetActive(true);
        noticeTxt.text = text;
        yield return new WaitForSeconds(time);
        noticePanel.SetActive(false);
    }

    public void OnJoinRoom(string rName)  //방 접속
    {
        //OnConnect();
        roomName_ = rName;
        StartCoroutine(Notice("방 접속중...", 1f));
        PhotonNetwork.JoinRoom(rName);
        PhotonNetwork.LoadLevel(rName);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.LoadLevel("Lobby");
    }


    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("방 생성 완료");
    }

    public override void OnJoinedRoom() //방 접속 성공
    {
        base.OnJoinedRoom();
       //PhotonNetwork.LoadLevel(roomName_);
        StartCoroutine(Notice(roomName_+"방 입장 성공",2f));
        Debug.Log("방 입장 성공");

        CreatePlayer();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("방 접속 실패");
        StartCoroutine(Notice("연결 오류!\n다시 접속합니다...",2f));
        CreateRoom(roomName_);
        OnJoinRoom(roomName_);
    }

    public void JoinRoomError()
    {
        StartCoroutine(Notice("오류 발생! 기다려주세요...", 3f));
        OnJoinRoom(roomName_);
    }

    void CreatePlayer()
    {
        string nowC = CharacterManager.instance.currentCharacter.ToString()+"_"+
            GameManager.instance.myGhostColor.ToString();
        GameObject pl = Resources.Load(nowC) as GameObject;
        TextMeshProUGUI nameT = pl.GetComponentInChildren<TextMeshProUGUI>();
        nameT.text = myName_;
        GameObject trans = GameObject.Find("SpawnPoint");
        myGhost = PhotonNetwork.Instantiate(nowC, trans.GetComponent<Transform>().position, Quaternion.identity);
        //photonView.RPC("SyncPlayer", RpcTarget.AllBuffered, myName_);
    }

    public bool CheckMaster()  //방장 체크
    {
        if (PhotonNetwork.IsMasterClient)
        {
            return true;
        }
        else
            return false;
    }
}
