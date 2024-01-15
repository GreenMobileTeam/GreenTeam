using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGManager : MonoBehaviour
{
    Scene scene;
    bool flag = true;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        scene = SceneManager.GetActiveScene();
        if(scene.name == "Lobby")
        {
            this.gameObject.GetComponent<AudioSource>().Stop();
            flag = false;
        }
        if(scene.name == "Main" && flag == false)
        {
            this.gameObject.GetComponent<AudioSource>().Play();
            flag = true;
        }
    }
}
