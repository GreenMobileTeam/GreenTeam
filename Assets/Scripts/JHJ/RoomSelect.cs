using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSelect : MonoBehaviour
{
    public void Camping()
    {
        ServerManager.instance.OnJoinRoom("Map_1");
    }

    public void Doom()
    {
        ServerManager.instance.OnJoinRoom("Map_2");
    }

    public void Pirate()
    {
        ServerManager.instance.OnJoinRoom("Map_3");
    }
}
