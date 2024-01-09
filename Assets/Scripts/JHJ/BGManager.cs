using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGManager : MonoBehaviour
{
    Scene scene;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        scene = SceneManager.GetActiveScene();
        if(scene.name == "Lobby_A")
        {
            Destroy(this.gameObject);
        }
    }
}
