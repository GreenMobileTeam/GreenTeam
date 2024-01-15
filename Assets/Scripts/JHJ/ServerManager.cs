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

    public void OnConnect() //�α��� �� ���� ���� ���� Ȯ��
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnDisconnect()   //���� ����
    {
        PhotonNetwork.Disconnect();
    }

    public void OnLeaveLobby()  //�κ񿡼� ����(�� ����, �α��� ȭ������ ������)
    {
        PhotonNetwork.LeaveLobby();
    }

    public override void OnLeftLobby()
    {
        base.OnLeftLobby();
        StopAllCoroutines();
        StartCoroutine(Notice("�κ� �������ϴ�.", 1f));
    }

    public override void OnJoinedLobby() //�κ� ����
    {
        StopAllCoroutines();
        StartCoroutine(Notice("�κ� ���� �Ϸ�!", 1f));
        base.OnJoinedLobby();
        PhotonNetwork.LoadLevel("Lobby");
        Debug.Log("�κ� ����");

        for (int i = 1; i < 3; i++)
            CreateRoom("Map_" + i);
    }

    public override void OnConnectedToMaster()  // ������ ���� ����
    {
        base.OnConnectedToMaster();
        StopAllCoroutines();
        StartCoroutine(Notice("���� ���� ����", 2f));
        Debug.Log("������ ���� ����");
    }

    public void GotoLobby() //�κ� ���� �õ�
    {
        SceneManager.LoadScene("Lobby");
        StopAllCoroutines();
        StartCoroutine(Notice("�κ� ������...", 3f));

        OnConnect();
        myName_ = GameManager.instance.myName;
        PhotonNetwork.NickName = myName_;
    }

    public void onDisconnect() //�ƿ� ����(�ٽ� �α��� ȭ��)
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        StartCoroutine(Notice("������ ������ϴ�", 1.2f));
    }

    IEnumerator Notice(string text, float time)
    {
        //Debug.Log("start");
        noticePanel.SetActive(true);
        noticeTxt.text = text;
        yield return new WaitForSeconds(time);
        noticePanel.SetActive(false);
    }

    public void OnJoinRoom(string rName)  //�� ����
    {
        //OnConnect();
        roomName_ = rName;
        StartCoroutine(Notice("�� ������...", 1f));
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
        Debug.Log("�� ���� �Ϸ�");
    }

    public override void OnJoinedRoom() //�� ���� ����
    {
        base.OnJoinedRoom();
       //PhotonNetwork.LoadLevel(roomName_);
        StartCoroutine(Notice(roomName_+"�� ���� ����",2f));
        Debug.Log("�� ���� ����");

        CreatePlayer();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("�� ���� ����");
        StartCoroutine(Notice("���� ����!\n�ٽ� �����մϴ�...",2f));
        CreateRoom(roomName_);
        OnJoinRoom(roomName_);
    }

    public void JoinRoomError()
    {
        StartCoroutine(Notice("���� �߻�! ��ٷ��ּ���...", 3f));
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

    public bool CheckMaster()  //���� üũ
    {
        if (PhotonNetwork.IsMasterClient)
        {
            return true;
        }
        else
            return false;
    }
}
