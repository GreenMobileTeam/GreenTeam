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
                if(GameManager.instance.isGuest)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                else
                {
                    LogOutManager.Instance.LogOut();
                }
            }
            else
            {
                SceneManager.LoadScene("Lobby");
            }
        }
#endif
    }
    void DoubleClick()
    {
        ClickCount = 0;
    }
}
