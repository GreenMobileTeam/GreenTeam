using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    int ClickCount = 0;

    private static BackButton instance = null;
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


    public static BackButton Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().buildIndex ==0)
            {
                ClickCount++;
                if (!IsInvoking("DoubleClick")) 
                    Invoke("DoubleClick", 1.0f);
                if (ClickCount == 2)
                {
                    CancelInvoke("DoubleClick");
                    Application.Quit();
                }
        }
            else if(SceneManager.GetActiveScene().buildIndex == 1)
            {
               SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
            else if(SceneManager.GetActiveScene().buildIndex == 2)
            {
                if (PlayerPrefs.GetInt("IsGuest") == 0)
                {
                     LogOutManager.Instance.LogOut(PlayerPrefs.GetString("Username"));
                }

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
            }
            else
            {
                SceneManager.LoadScene("Lobby_A");
            }
        }
#endif
    }
    void DoubleClick()
    {
        ClickCount = 0;
    }
}
