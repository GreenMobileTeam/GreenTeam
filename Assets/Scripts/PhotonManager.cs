using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public GameObject playerGhost;
    private readonly string version = "1.0";
    //private string userId = "Yong";
    bool flag = false;
    public Material[] gColors;
    public GameObject[] gPrefabs;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = version;
        //PhotonNetwork.NickName = PlayerPrefs.GetString("Name");
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
        //Debug.Log($"{PhotonNetwork.InRoom}");
        //Debug.Log($"{PhotonNetwork.CurrentRoom.PlayerCount}");
        Transform spawnpoint = GameObject.Find("SpawnPoint").GetComponent<Transform>();
        switch (CharacterManager.instance.currentCharacter)
        {
            case Character.ghostA:
                ChangeColor(gPrefabs[0],gColors[PlayerPrefs.GetInt("gColor")]);
                playerGhost = PhotonNetwork.Instantiate("GhostA", spawnpoint.position, spawnpoint.rotation, 0);
                //playerGhost = PhotonNetwork.Instantiate("Test", spawnpoint.position, spawnpoint.rotation, 0);
                break;
            case Character.ghostB:
                ChangeColor(gPrefabs[1], gColors[PlayerPrefs.GetInt("gColor")]);
                playerGhost = PhotonNetwork.Instantiate("GhostB", spawnpoint.position, spawnpoint.rotation, 0);
                break;
            case Character.ghostC:
                ChangeColor(gPrefabs[2], gColors[PlayerPrefs.GetInt("gColor")]);
                playerGhost = PhotonNetwork.Instantiate("GhostC", spawnpoint.position, spawnpoint.rotation, 0);
                break;
        }

        /*
        foreach(var player in PhotonNetwork.CurrentRoom.Players)
        {
            //Debug.Log($"{player.Value.NickName}, {player.Value.ActorNumber}");
            Debug.Log("Spawn"+player);
            Transform spawnpoint = GameObject.Find("SpawnPoint").GetComponent<Transform>();

            if (CharacterManager.instance.currentCharacter == Character.ghostA && flag == false)
            {
                flag = true;
                playerGhost = PhotonNetwork.Instantiate("GhostA", spawnpoint.position, spawnpoint.rotation, 0);
                flag = false;
            } 
            else if (CharacterManager.instance.currentCharacter == Character.ghostB && flag == false)
            {
                flag = true;
                playerGhost = PhotonNetwork.Instantiate("GhostB", spawnpoint.position, spawnpoint.rotation, 0);
                flag = false;
            }
            else
            {
                if(flag == false)
                {
                    flag = true;
                    playerGhost = PhotonNetwork.Instantiate("GhostC", spawnpoint.position, spawnpoint.rotation, 0);
                    flag = false;
                }
            }
        }
        */
    }

    void ChangeColor(GameObject prefabPlayer, Material material)
    {
        Debug.Log("Color Changed");
        Renderer prefabPlayerRenderer = prefabPlayer.GetComponentInChildren<Renderer>();

        if (prefabPlayerRenderer != null)
        {
            prefabPlayerRenderer.material = material;
        }
    }
}
