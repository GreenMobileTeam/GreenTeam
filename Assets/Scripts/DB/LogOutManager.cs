using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LogOutManager : MonoBehaviour
{
    string serverURL = "http://greenacademi.store";

    string username;

    private static LogOutManager instance = null;

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

    public static LogOutManager Instance
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

    public void LogOut()
    {
        StartCoroutine(SendLogoutRequest());
    }

    public void LogOutTemp()
    {
        StartCoroutine(SendLogoutRequestTemp());
    }

    IEnumerator SendLogoutRequest()
    {
        string url = $"{serverURL}/logout";

        WWWForm form = new WWWForm();

        username = GameManager.instance.myID;
        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("로그아웃 성공");
            }
            else
            {
                Debug.LogError("서버 오류 : " + www.error);
            }
        }
    }

    IEnumerator SendLogoutRequestTemp()
    {
        string url = $"{serverURL}/logout";

        WWWForm form = new WWWForm();

        username = GameManager.instance.myID;
        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("로그아웃 성공");
                SceneManager.LoadScene("Main");
            }
            else
            {
                Debug.LogError("서버 오류 : " + www.error);
            }
        }
    }

    public void OnApplicationQuit()
    {
        if (!GameManager.instance.isGuest)
            LogOutTemp();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            if (!GameManager.instance.isGuest)
                LogOutTemp();
        }
        else
        {
        }
    }

}
