using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public Button sendBtn;
    public TextMeshProUGUI chatLog;
    public TextMeshProUGUI chattingList;
    public TMP_InputField input;
    public ScrollRect scroll_rect;
    string chatters;
    string color;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        //scroll_rect = GameObject.FindObjectOfType();
        color = PlayerPrefs.GetString("Mycolor");
    }
    public void SendButtonOnClicked()
    {
        if (input.text.Equals("")) { Debug.Log("Empty"); return; }
        string msg = string.Format("<color=#{0}>[{1}]</color> {2}",color , PhotonNetwork.LocalPlayer.NickName, input.text);
        photonView.RPC("ReceiveMsg", RpcTarget.OthersBuffered, msg);
        ReceiveMsg(msg);
        input.ActivateInputField(); // π›¥Î¥¬ input.select(); (π›¥Î∑Œ ≈‰±€)
        input.text = "";
    }
    void Update()
    {
        chatterUpdate();
        if (Input.GetKeyDown(KeyCode.Return) && !input.isFocused) SendButtonOnClicked();
    }
    void chatterUpdate()
    {
        chatters = "Player List\n";
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            string s = "";
            if (PhotonNetwork.LocalPlayer.NickName == p.NickName)
            {
                s = string.Format("<color=#FFFF00>[{0}]\n", p.NickName);
            }
            else
            {
                s = string.Format("<color=#FFFFFF>[{0}]\n", p.NickName);
            }
            chatters += s;
        }
        chattingList.text = chatters;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //base.OnPlayerEnteredRoom(newPlayer);
        string msg = string.Format("<color=#00ff00>[{0}]¥‘¿Ã ¿‘¿Â«œºÃΩ¿¥œ¥Ÿ.</color>", newPlayer.NickName);
        ReceiveMsg(msg);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //base.OnPlayerLeftRoom(otherPlayer);
        string msg = string.Format("<color=#ff0000>[{0}]¥‘¿Ã ≈¿Â«œºÃΩ¿¥œ¥Ÿ.</color>", otherPlayer.NickName);
        ReceiveMsg(msg);
    }

    [PunRPC]
    public void ReceiveMsg(string msg)
    {
        chatLog.text += "\n" + msg;
        scroll_rect.verticalNormalizedPosition = 0.0f;
    }
}