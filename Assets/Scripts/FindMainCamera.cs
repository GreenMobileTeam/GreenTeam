using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FindMainCamera : MonoBehaviourPunCallbacks
{
    private GameObject mainCamera;
    private PlayerCamera mainCamera_script;
    private Transform myTR;
    private PhotonView myPV;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera_script = mainCamera.GetComponent<PlayerCamera>();

        myTR = GetComponent<Transform>();
        myPV = GetComponent<PhotonView>();

        if (myPV.IsMine)
        {
            //mainCamera_script.PlayerTransform = myTR.transform;
        }
    }

    void Update()
    {
        
    }
}
