using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Collections;

public class PlayerName : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI name_;
    PhotonView pv;

    private IEnumerator Start()
    {
        name_.text = "이름 로딩중";
        pv = GetComponent<PhotonView>();
        yield return new WaitForSeconds(3f);
        if (pv.IsMine)
        {
            string n = PlayerPrefs.GetString("GhostName");
            name_.text = n;
            pv.RPC("UpdateNick", RpcTarget.All, n);
            UpdateNick(n);
        }
    }

    [PunRPC]
    void UpdateNick(string name)
    {
        name_.text = name;
    }
}
