using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Collections;

public class PlayerName : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI name_;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        if (PlayerPrefs.GetInt("IsGuest") == 1)
        {
            string n = PhotonNetwork.LocalPlayer.NickName;
            name_.text = n;
        }
        else
        {
            string[] temp = PlayerPrefs.GetString("Nickname").Split(":");
            string n = temp[1].Substring(1, temp[1].Length - 3);

            if (n == PhotonNetwork.LocalPlayer.NickName)
            {
                name_.text = n;
            }
        }
    }
}
