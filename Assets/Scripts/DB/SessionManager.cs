using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SessionManager : MonoBehaviour
{
    public string serverURL = "http://localhost:3000";

    public void Logout()
    {
        StartCoroutine(SendLogoutRequest(PlayerPrefs.GetString("Username")));
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
                Debug.LogError("로그아웃 오류: " + www.error);
            }
        }
    }
}
