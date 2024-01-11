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
        Debug.Log(GameManager.instance.isGuest);
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

        username = PlayerPrefs.GetString("Username");
        Debug.LogWarning(username);
        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();


            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("�α׾ƿ� ����");
                SceneManager.LoadScene("Main");
            }
            else
            {
                Debug.LogError("���� ���� : " + www.error);
            }
        }
    }
}
