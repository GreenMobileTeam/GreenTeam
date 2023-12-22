using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public Button loginBtn;
    public TextMeshProUGUI IDtext;
    public TextMeshProUGUI connetState;
    public TextMeshProUGUI lenghtText;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        loginBtn.interactable = false;
        connetState.text = "Connectiong to Master Server...";
    }

    private void Update()
    {
        if (IDtext.text.Length <= 1 || IDtext.text.Length >10)
        {
            loginBtn.interactable = false;
            lenghtText.text = "Text length: 2~10";
        }
        else {
            loginBtn.interactable = true;
            lenghtText.text = "Good";
        }
    }

    public void Connect()
    {
        //if(IDtext.text.Equals(""))
        if(IDtext.text.Length == 0)
        {
            return;
        }
        else
        {
            PhotonNetwork.LocalPlayer.NickName = IDtext.text;
            loginBtn.interactable = false;
            if (PhotonNetwork.IsConnected)
            {
                connetState.text = "Connecting to room...";
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                connetState.text = "Offline: failed to connect.\nReconnecting...";
                PhotonNetwork.ConnectUsingSettings();
            }
        }
    }

    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();
        loginBtn.interactable = true;
        connetState.text = "Online: connect to master server";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //base.OnDisconnected(cause);
        loginBtn.interactable = false;
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
        PhotonNetwork.LoadLevel("Main");
    }
}
