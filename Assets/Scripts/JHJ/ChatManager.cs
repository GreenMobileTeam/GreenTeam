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
    public GameObject titleSendBtn;
    public TextMeshProUGUI chatLog;
    public TextMeshProUGUI chattingList;
    public TextMeshProUGUI filterWord;
    public TextMeshProUGUI title;
    public GameObject titleInput;
    public TMP_InputField input;
    public ScrollRect scroll_rect;
    public GameObject winnerBtn;
    public GameObject winnerPan;

    TMP_InputField titleInput_;

    string chatters;
    string color;
    string inMsg;
    string[] wordList;

    void Start()
    {
        wordList = filterWord.text.Split(", ");
        PhotonNetwork.IsMessageQueueRunning = true;
        //scroll_rect = GameObject.FindObjectOfType();
        if (!PlayerPrefs.HasKey("Mycolor"))
            color = "FFFFFF";
        else
            color = PlayerPrefs.GetString("Mycolor");
        titleInput_ = titleInput.GetComponent<TMP_InputField>();
    }

    public void SendButtonOnClicked()
    {
        if (input.text.Equals("")) { Debug.Log("Empty"); return; }
        string msg = "";
        inMsg = input.text;
        MsgDetect();
        if (PhotonNetwork.IsMasterClient)  //�����̶��
        {
            msg = string.Format("<color=#{0}>[��{1}]</color> {2}", color, PhotonNetwork.LocalPlayer.NickName, inMsg);
        }
        else
        {
            msg = string.Format("<color=#{0}>[{1}]</color> {2}", color, PhotonNetwork.LocalPlayer.NickName, inMsg);
        }
        photonView.RPC("ReceiveMsg", RpcTarget.OthersBuffered, msg);
        ReceiveMsg(msg);
        input.ActivateInputField(); // �ݴ�� input.select(); (�ݴ�� ���)
        input.text = "";
    }

    public void TitleUpdate_()
    {
        photonView.RPC("TitleUpdate", RpcTarget.OthersBuffered, titleInput_.text);
        TitleUpdate(titleInput_.text);
        titleInput_.ActivateInputField();
    }

    void Update()
    {
        color = PlayerPrefs.GetString("Mycolor");
        chatterUpdate();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TitleUpdate_();
            SendButtonOnClicked();
        }
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
        string msg = string.Format("<color=#00ff00>[{0}]���� �����ϼ̽��ϴ�.</color>", newPlayer.NickName);
        ReceiveMsg(msg);

        if (PhotonNetwork.IsMasterClient) //�����̶�� Ÿ��Ʋ �Է�ĭ, ��� ���� ��
        {
            titleInput.SetActive(true);
            titleSendBtn.SetActive(true);
            winnerBtn.SetActive(true);
            winnerPan.SetActive(true);
        }
        else
        {
            titleInput.SetActive(false);
            titleSendBtn.SetActive(false);
            winnerBtn.SetActive(false);
            winnerPan.SetActive(false);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //base.OnPlayerLeftRoom(otherPlayer);
        string msg = string.Format("<color=#ff0000>[{0}]���� �����ϼ̽��ϴ�.</color>", otherPlayer.NickName);
        ReceiveMsg(msg);

        if (PhotonNetwork.IsMasterClient) //�����̶�� Ÿ��Ʋ �Է�ĭ, ��� ���� ��
        {
            titleInput.SetActive(true);
            titleSendBtn.SetActive(true);
            winnerBtn.SetActive(true);
            winnerPan.SetActive(true);
        }
        else
        {
            titleInput.SetActive(false);
            titleSendBtn.SetActive(false);
            winnerBtn.SetActive(false);
            winnerPan.SetActive(false);
        }
    }

    public void GoOut()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Lobby_A");
    }

    void MsgDetect()   //��Ӿ� ����
    {
        char[] worr = inMsg.ToCharArray();
        //Debug.Log(inMsg);

        for(int i = 0; i < wordList.Length; i++)
        {
            char[] filter = wordList[i].ToCharArray();
            //Debug.Log(filter[0]);
            for(int j = 0; j < worr.Length-1; j++)
            {
                if (worr[j] == filter[0])
                {
                    if (filter.Length == 1)
                    {
                        Debug.Log(worr[j] + "-> ��");
                        worr[j] = '��';
                    }
                    else
                    {
                        if(worr[j+1] == filter[1])
                        {
                            Debug.Log(worr[j] + worr[j + 1] + "-> �߿�");
                            worr[j] = '��';
                            worr[j + 1] = '��';
                        }
                    }
                }
            }
            if (worr[worr.Length - 1] == filter[0] && wordList[i].Length ==1)
            {
                worr[worr.Length - 1] = '��';
            }
        }

        inMsg = new string(worr);
    }

    public void WinnerBlue()
    {
        WinUpdate("����", "0000FF");
    }

    public void WinnerRed()
    {
        WinUpdate("�ݴ�", "FF0000");
    }

    public void WinnerNone()
    {
        string n = string.Format("���º� �Դϴ�~!");
        string msg = string.Format("<color=#{0}>[��{1}] {2}</color>", "FFFFFF", PhotonNetwork.LocalPlayer.NickName, n);
        photonView.RPC("ReceiveMsg", RpcTarget.OthersBuffered, msg);
        ReceiveMsg(msg);
        input.ActivateInputField(); // �ݴ�� input.select(); (�ݴ�� ���)
        input.text = "";
    }

    

    void WinUpdate(string winner, string color)
    {
        string n = string.Format("�¸��� ���� {0}�� �Դϴ�~!",winner);
        string msg = string.Format("<color=#{0}>[��{1}] {2}</color>", color, PhotonNetwork.LocalPlayer.NickName, n);
        photonView.RPC("ReceiveMsg", RpcTarget.OthersBuffered, msg);
        ReceiveMsg(msg);
        input.ActivateInputField(); // �ݴ�� input.select(); (�ݴ�� ���)
        input.text = "";
    }

    [PunRPC]
    public void ReceiveMsg(string msg)
    {
        chatLog.text += "\n" + msg;
        scroll_rect.verticalNormalizedPosition = 0.0f;
    }

    [PunRPC]
    public void TitleUpdate(string txt)
    {
        //Debug.Log(txt);
        title.text = txt;
    }
}