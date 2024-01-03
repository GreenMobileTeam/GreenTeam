using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SessionManager : MonoBehaviour
{
    string serverURL = "http://localhost:3000";
    public float checkSessionInterval = 0.1f;

    private void Start()
    {
        StartCoroutine(AutoLogout());
    }

    IEnumerator AutoLogout()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkSessionInterval);
            yield return CheckAndAutoLogout();
        }
    }

    IEnumerator CheckAndAutoLogout()
    {
        string username = PlayerPrefs.GetString("Username", "");

        string url = $"{serverURL}/checkSession";
        WWWForm form = new WWWForm();
        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                bool isLogged = JsonUtility.FromJson<SessionResponse>(www.downloadHandler.text).isLogged;

                if (!isLogged)
                {
                    yield return AutoLogoutRequest(username);
                }
            }
            else
            {
                Debug.LogError("서버 오류");
            }
        }
    }

    IEnumerator AutoLogoutRequest(string username)
    {
        string url = $"{serverURL}/logout";

        WWWForm form = new WWWForm();
        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("로그아웃");
            }
            else
            {
                Debug.LogError("로그아웃 오류: " + www.error);
            }
        }
    }

    [System.Serializable]
    public class SessionResponse
    {
        public bool isLogged;
    }
}
