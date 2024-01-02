using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class RoomInfoUpdater : MonoBehaviourPunCallbacks
{
    public int roomNumber;
    private void Update()
    {
        string key = "Room" + roomNumber + "Cu";
        Debug.Log(key);
        PlayerPrefs.SetInt(key, PhotonNetwork.CurrentRoom.PlayerCount);
    }
}
