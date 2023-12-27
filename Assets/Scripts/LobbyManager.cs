using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI IDtext;
    public TextMeshProUGUI connetState;
    public TextMeshProUGUI lenghtText;

    int roomNum = 1;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        connetState.text = "Connectiong to Master Server...";
    }

    private void Update()
    {
        if (IDtext.text.Length <= 1 || IDtext.text.Length >20)
        {
            lenghtText.text = "Text length: 2~10";
        }
        else {
            lenghtText.text = "Available name";
        }
    }

    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();
        connetState.text = "Online: connect to master server";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //base.OnDisconnected(cause);
        connetState.text = "Offline: failed to connect.\nReconnecting...";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //base.OnJoinRandomFailed(returnCode, message);
        connetState.text = "No empty room. Creating new room...";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 7 });
    }

    public override void OnJoinedRoom()
    {
        //base.OnJoinedRoom();
        connetState.text = "Succes to join room";
        switch (roomNum)
        {
            case 1:
                PhotonNetwork.LoadLevel("ChatRoom1");
                break;
            case 2:
                PhotonNetwork.LoadLevel("ChatRoom2");
                break;
            case 3:
                PhotonNetwork.LoadLevel("ChatRoom3");
                break;
            default:
                break;
        }
    }

    //rooms
    public void ConnectRoom1()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LocalPlayer.NickName = IDtext.text;
            connetState.text = "Connecting to ChatRoom1...";
            //PhotonNetwork.LoadLevel("ChatRoom1");
            roomNum = 1;
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connetState.text = "Offline: failed to connect.\nReconnecting...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void ConnectRoom2()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LocalPlayer.NickName = IDtext.text;

            connetState.text = "Connecting to ChatRoom2...";
            //PhotonNetwork.LoadLevel("ChatRoom2");
            roomNum = 2;
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connetState.text = "Offline: failed to connect.\nReconnecting...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void ConnectRoom3()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LocalPlayer.NickName = IDtext.text;

            connetState.text = "Connecting to ChatRoom3...";
            //PhotonNetwork.LoadLevel("ChatRoom3");
            roomNum = 3;
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connetState.text = "Offline: failed to connect.\nReconnecting...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}
