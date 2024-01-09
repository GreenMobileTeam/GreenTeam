using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
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
        PhotonNetwork.CreateRoom("Map_" + roomNum, new RoomOptions { MaxPlayers = 10 });

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //base.OnJoinRandomFailed(returnCode, message);
        //connetState.text = "No empty room. Creating new room...";
        Debug.Log("Room is not existing");
        PhotonNetwork.CreateRoom("Map_" + roomNum, new RoomOptions { MaxPlayers = 10 });
    }

    public override void OnJoinedRoom()
    {
        //base.OnJoinedRoom();
        //connetState.text = "Succes to join room";
        //string temp = "Map_" + roomNum;
        //PhotonNetwork.LoadLevel(temp);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("Connecting Room" +roomNum+ " failed. Reconnecting...");
        PhotonNetwork.CreateRoom("Map_" + roomNum, new RoomOptions { MaxPlayers = 10 });
    }

    //rooms
    public void Room1Secelct()
    {
        ConnectRoom("Map_1");
        SceneManager.LoadScene("Map_1");
    }

    public void Room2Secelct()
    {
        ConnectRoom("Map_2");
        SceneManager.LoadScene("Map_2");
    }

    public void Room3Secelct()
    {
        ConnectRoom("Map_3");
        SceneManager.LoadScene("Map_3");
    }

    public void LogOut()
    {
        SceneManager.LoadScene("Login");
    }

    void ConnectRoom(string roomName)
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Connecting Room" +roomNum+"...");
            string n_ = PlayerPrefs.GetString("Nickname");
            Debug.Log(PlayerPrefs.GetString("Nickname"));
            if (PlayerPrefs.GetInt("IsGuest") == 1)
            {
                string n = "Tester" + Random.Range(0, 101);
                PhotonNetwork.LocalPlayer.NickName = n;
                PlayerPrefs.SetString("GuestName", n);
            }
            else
            {
                if (n_ == "" || n_ == " ")
                {
                    Debug.Log("Name Error");
                }
                else
                {
                    string[] temp = n_.Split(":");
                    Debug.Log(temp[0] + temp[1]);
                    string t = temp[1].Substring(1, temp[1].Length - 3);
                    PhotonNetwork.LocalPlayer.NickName = t;
                }
            }
            //PhotonNetwork.JoinRandomRoom();
            PhotonNetwork.JoinOrCreateRoom("Map_" + roomNum, new RoomOptions { MaxPlayers = 10}, null);
            PhotonNetwork.LoadLevel(roomName);
        }
        else
        {
            Debug.Log("Failed");
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}
