using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LogoutBtn : MonoBehaviour
{
    string serverURL = "http://localhost:3000";
    string username;
    // Start is called before the first frame update
    public void LogOut()
    {
        if (PlayerPrefs.GetInt("IsGuest") == 0)
        {
            StartCoroutine(SendLogoutRequest());
        }
    }

    IEnumerator SendLogoutRequest()
    {
        string url = $"{serverURL}/logout";

        WWWForm form = new WWWForm();

        username = PlayerPrefs.GetString("Username");
        Debug.LogWarning(username);
        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();


            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("로그아웃 성공");

                SceneManager.LoadScene("login");
            }
            else
            {
                Debug.LogError("서버 오류 : " + www.error);
            }
        }
    }
}
