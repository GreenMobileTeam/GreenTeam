using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    //public TextMeshProUGUI IDtext;
    //public TextMeshProUGUI connetState;
    //public TextMeshProUGUI lenghtText;
    Dictionary<string, GameObject> roomDict = new Dictionary<string, GameObject>();

    int roomNum = 1;
    public int rooms;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        //connetState.text = "Connectiong to Master Server...";
    }

    private void Update()
    {
        /*
        if (IDtext.text.Length <= 1 || IDtext.text.Length >20)
        {
            lenghtText.text = "Text length: 2~10";
        }
        else {
            lenghtText.text = "Available name";
        }
        */
    }

    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();
        //connetState.text = "Online: connect to master server";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //base.OnDisconnected(cause);
        //connetState.text = "Offline: failed to connect.\nReconnecting...";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
       // base.OnCreateRoomFailed(returnCode, message);
        PhotonNetwork.CreateRoom("ChatRoom" + roomNum, new RoomOptions { MaxPlayers = 10 });

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //base.OnJoinRandomFailed(returnCode, message);
        //connetState.text = "No empty room. Creating new room...";
        Debug.Log("Room is not existing");
        PhotonNetwork.CreateRoom("ChatRoom"+roomNum, new RoomOptions { MaxPlayers = 10 });
    }

    public override void OnJoinedRoom()
    {
        //base.OnJoinedRoom();
        //connetState.text = "Succes to join room";
        string temp = "ChatRoom" + roomNum;
        PhotonNetwork.LoadLevel(temp);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("Connecting Room" +roomNum+ " failed. Reconnecting...");
        PhotonNetwork.CreateRoom("ChatRoom" + roomNum, new RoomOptions { MaxPlayers = 10 });
    }

    //rooms
    public void RoomSelect()
    {
        GameObject clickedBtn = EventSystem.current.currentSelectedGameObject;
        string name = clickedBtn.name;
        roomNum = int.Parse(name.Substring(name.Length-1,1));
        Debug.Log("Select" + roomNum + "Room");
        ConnectRoom();
    }

    void ConnectRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Connecting Room" +roomNum+"...");
            PhotonNetwork.LocalPlayer.NickName = "Tester" + Random.Range(0, 101);
            //PhotonNetwork.JoinRandomRoom();
            PhotonNetwork.JoinOrCreateRoom("ChatRoom" + roomNum, new RoomOptions { MaxPlayers = 10}, null);
            PhotonNetwork.LoadLevel("ChatRoom"+roomNum);
        }
        else
        {
            Debug.Log("Failed");
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}
