using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGManager : MonoBehaviour
{
    Scene scene;

    private static BGManager instance = null;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        scene = SceneManager.GetActiveScene();
        if(scene.name == "Lobby")
        {
            Destroy(this.gameObject);
        }
    }
}
