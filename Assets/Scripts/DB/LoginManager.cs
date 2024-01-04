using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public Button loginButn;

    private bool nullCheck;
    public GameObject popup;

    private string serverURL = "http://localhost:3000";

    private void Awake()
    {
        nullCheck = false;
    }

    private void Update()
    {
        if (nullCheck = NullCheck())
        {
            loginButn.interactable = true;
        }
        else
        {
            loginButn.interactable = false;
        }
    }

    private bool NullCheck()
    {
        if (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(passwordInput.text))
        {
            return false;
        }
        return true;
    }

    public class LoginResponse
    {
        public string message;
    }


    public void LogIn()
    {
        StartCoroutine(SendLogInRequest(usernameInput.text, passwordInput.text));
    }

    IEnumerator SendLogInRequest(string username, string password)
    {
        string url = $"{serverURL}/login";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success || request.responseCode == 401)
            {
                string jsonResponse = request.downloadHandler.text;

                try
                {
                    LoginResponse response = JsonUtility.FromJson<LoginResponse>(jsonResponse);

                    if (response.message == "success")
                    {
                        OnLoginSuccess(username);
                        SceneManager.LoadScene("Lobby_A");
                    }
                    else if (response.message == "username" || response.message == "password")
                    {
                        popup.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("오류");
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("에러" + e.Message);
                }
            }
            else
            {
                Debug.LogError("Login" + request.error);
            }
        }
    }

    IEnumerator GetLoginInfo(string username)
    {
        string url = $"{serverURL}/getLoginInfo";
        WWWForm form = new WWWForm();
        form.AddField("username", username);

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string response = request.downloadHandler.text;
                string nickname = ParseNicknameFromResponse(response);

                PlayerPrefs.SetString("Nickname", nickname);
                PlayerPrefs.SetString("Username", username);

                string savedNickname = PlayerPrefs.GetString("Nickname", "DefaultNickname");
                Debug.Log("현재 닉네임: " + savedNickname);
                PlayerPrefs.SetString("Name", savedNickname);  //혜진
                PlayerPrefs.SetInt("IsGuest", 0);
                Debug.Log(PlayerPrefs.GetString("Name"));
                SceneManager.LoadScene("Lobby_A");
            }
            else
            {
                Debug.LogError("GetLoginInfo:" + request.error);
            }
        }
    }

    string ParseNicknameFromResponse(string response)
    {
        return response.Trim();
    }

    void OnLoginSuccess(string username)
    {
        StartCoroutine(GetLoginInfo(username));
    }

    public void PopUpClose()
    {
        popup.SetActive(false);
    }

    public void GuestLogin()
    {
        SceneManager.LoadScene("Lobby_A");
        PlayerPrefs.SetInt("IsGuest", 1);
    }

}
