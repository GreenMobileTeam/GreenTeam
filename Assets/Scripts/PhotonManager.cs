using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public GameObject playerGhost;
    private readonly string version = "1.0";
    private string userId = "Yong";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = version;
        PhotonNetwork.NickName = userId;
        Debug.Log(PhotonNetwork.SendRate);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Matset");
        base.OnConnectedToMaster();
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"JoinRandom Faild {returnCode}:{message}");

        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20;
        ro.IsOpen = true;
        ro.IsVisible = true;

        PhotonNetwork.CreateRoom("New Room", ro);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"{PhotonNetwork.InRoom}");
        Debug.Log($"{PhotonNetwork.CurrentRoom.PlayerCount}");

        foreach(var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"{player.Value.NickName}, {player.Value.ActorNumber}");

            Transform spawnpoint = GameObject.Find("SpawnPoint").GetComponent<Transform>();

            if(CharacterManager.instance.currentCharacter == Character.ghostA)
            {
                playerGhost = PhotonNetwork.Instantiate("GhostA", spawnpoint.position, spawnpoint.rotation, 0);
            } 
            else if (CharacterManager.instance.currentCharacter == Character.ghostB)
            {
                playerGhost = PhotonNetwork.Instantiate("GhostB", spawnpoint.position, spawnpoint.rotation, 0);
            }
            else
            {
                playerGhost = PhotonNetwork.Instantiate("GhostC", spawnpoint.position, spawnpoint.rotation, 0);
            }
        }
    }
}
