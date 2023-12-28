using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public int roomNumber;
    public Button sendBtn;
    public TextMeshProUGUI chatLog;
    public TextMeshProUGUI chattingList;
    public TMP_InputField input;
    public ScrollRect scroll_rect;
    string chatters;
    string color;
    string inMsg;
    string[] wordList = { "시발", "새끼", "섹스", "병신", "애미",
                                        "느금", "애비", "년", "좆", "ㅗ"};

    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        //scroll_rect = GameObject.FindObjectOfType();
        color = PlayerPrefs.GetString("Mycolor");
    }

    public void SendButtonOnClicked()
    {
        if (input.text.Equals("")) { Debug.Log("Empty"); return; }
        string msg = "";
        inMsg = input.text;
        MsgDetect();
        if (PhotonNetwork.IsMasterClient)  //방장이라면
        {
            msg = string.Format("<color=#{0}>[☆{1}] {2}</color>", color, PhotonNetwork.LocalPlayer.NickName, inMsg);
        }
        else
        {
            msg = string.Format("<color=#{0}>[{1}]</color> {2}", color, PhotonNetwork.LocalPlayer.NickName, inMsg);
        }
        photonView.RPC("ReceiveMsg", RpcTarget.OthersBuffered, msg);
        ReceiveMsg(msg);
        input.ActivateInputField(); // 반대는 input.select(); (반대로 토글)
        input.text = "";
    }

    void Update()
    {
        color = PlayerPrefs.GetString("Mycolor");
        chatterUpdate();
        if (Input.GetKeyDown(KeyCode.Return)) SendButtonOnClicked();
    }

    void chatterUpdate()
    {
        chatters = "Player List\n";
        PlayerPrefs.SetInt("Room" + roomNumber, PhotonNetwork.PlayerList.Length);
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
        string msg = string.Format("<color=#00ff00>[{0}]님이 입장하셨습니다.</color>", newPlayer.NickName);
        ReceiveMsg(msg);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //base.OnPlayerLeftRoom(otherPlayer);
        string msg = string.Format("<color=#ff0000>[{0}]님이 퇴장하셨습니다.</color>", otherPlayer.NickName);
        ReceiveMsg(msg);
    }

    public void GoOut()
    {
        PhotonNetwork.LoadLevel("Lobby_A");

    }

    void MsgDetect()   //비속어 필터
    {
        char[] worr = inMsg.ToCharArray();
        Debug.Log(inMsg);

        for(int i = 0; i < wordList.Length; i++)
        {
            char[] filter = wordList[i].ToCharArray();
            Debug.Log(filter[0]);
            for(int j = 0; j < worr.Length-1; j++)
            {
                if (worr[j] == filter[0])
                {
                    if (filter.Length == 1)
                    {
                        Debug.Log(worr[j] + "-> 냥");
                        worr[j] = '냥';
                    }
                    else
                    {
                        if(worr[j+1] == filter[1])
                        {
                            Debug.Log(worr[j] + worr[j + 1] + "-> 야옹");
                            worr[j] = '야';
                            worr[j + 1] = '옹';
                        }
                    }
                }
            }
            if (worr[worr.Length - 1] == filter[0] && wordList[i].Length ==1)
            {
                worr[worr.Length - 1] = '♡';
            }
        }

        inMsg = new string(worr);
    }

    [PunRPC]
    public void ReceiveMsg(string msg)
    {
        chatLog.text += "\n" + msg;
        scroll_rect.verticalNormalizedPosition = 0.0f;
    }
}