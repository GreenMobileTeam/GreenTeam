using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LogOutManager : MonoBehaviour
{
    string serverURL = "http://localhost:3000";

    string username;

    private static LogOutManager instance = null;

    internal void LogOut()
    {
        throw new NotImplementedException();
    }

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

    public void LogOut(string username)
    {
        StartCoroutine(SendLogoutRequest(username));
    }

    IEnumerator SendLogoutRequest(string username)
    {
        string url = $"{serverURL}/logout";

        WWWForm form = new WWWForm();
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

    public void OnApplicationQuit()
    {
        if(PlayerPrefs.GetInt("IsGuest") == 0)
            LogOut(PlayerPrefs.GetString("Username"));
    }
}
