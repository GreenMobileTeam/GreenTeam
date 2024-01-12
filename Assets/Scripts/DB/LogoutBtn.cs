using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LogoutBtn : MonoBehaviour
{
    string serverURL = "http://greenacademi.store";
    string username;
    // Start is called before the first frame update
    public void LogOut()
    {
        ServerManager.instance.OnDisconnect();

        if (!GameManager.instance.isGuest)
        {
            StartCoroutine(SendLogoutRequest());
        }
        else
        {
            SceneManager.LoadScene("Main");
        }
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

                SceneManager.LoadScene("Main");
            }
            else
            {
                Debug.LogError("서버 오류 : " + www.error);
            }
        }
    }
}
