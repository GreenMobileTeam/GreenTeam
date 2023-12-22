using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public Button ColorEnd;
    public TextMeshProUGUI IDtext;
    public TextMeshProUGUI connetState;
    public TextMeshProUGUI lenghtText;

    public GameObject ColorSelectPanel;
    public GameObject RoomSelectPanel;
    public Transform RoomButtonPoz;
    public Button RoomButton;

    private void Start()
    {
        ColorSelectPanel.transform.localPosition = new Vector3(0, 0, 0);
        PhotonNetwork.ConnectUsingSettings();
        ColorEnd.interactable = false;
        connetState.text = "Connectiong to Master Server...";
    }

    private void Update()
    {
        if (IDtext.text.Length <= 1 || IDtext.text.Length >10)
        {
            ColorEnd.interactable = false;
            lenghtText.text = "Text length: 2~10";
        }
        else {
            ColorEnd.interactable = true;
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
            ColorEnd.interactable = false;
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
        ColorEnd.interactable = true;
        connetState.text = "Online: connect to master server";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //base.OnDisconnected(cause);
        ColorEnd.interactable = false;
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
        PhotonNetwork.LoadLevel("ChatRoom1");
    }

    //color select
    public void RedSelect()
    {
        //gm.myColor = "FF0000";
        PlayerPrefs.SetString("Mycolor", "FF0000");
    }

    public void YellowSelect()
    {
        //gm.myColor = "FFFF00";
        PlayerPrefs.SetString("Mycolor", "FFFF00");
    }

    public void LimeSelect()
    {
        //gm.myColor = "00FF00";
        PlayerPrefs.SetString("Mycolor", "00FF00");
    }

    public void AquaSelect()
    {
        //gm.myColor = "00FF00";
        PlayerPrefs.SetString("Mycolor", "00FFFF");
    }

    public void BlueSelect()
    {
        //gm.myColor = "00FF00";
        PlayerPrefs.SetString("Mycolor", "0000FF");
    }

    public void MagentaSelect()
    {
        //gm.myColor = "00FF00";
        PlayerPrefs.SetString("Mycolor", "FF00FF");
    }

    public void BlackSelect()
    {
        //gm.myColor = "00FF00";
        PlayerPrefs.SetString("Mycolor", "000000");
    }

    public void GraySelect()
    {
        //gm.myColor = "00FF00";
        PlayerPrefs.SetString("Mycolor", "808080");
    }

    public void WhiteSelect()
    {
        //gm.myColor = "00FF00";
        PlayerPrefs.SetString("Mycolor", "FFFFFF");
    }

    //Button select
    public void ColorSelectEnd()
    {
        ColorSelectPanel.transform.localPosition = new Vector3(-2000, 0, 0);
        RoomSelectPanel.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void BackToColor()
    {
        RoomSelectPanel.transform.localPosition = new Vector3(-2000, 0, 0);
        ColorSelectPanel.transform.localPosition = new Vector3(0, 0, 0);
    }


}
