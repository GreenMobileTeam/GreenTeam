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

    private string serverURL = "https://lemon-badgers-pay.loca.lt";

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

    [System.Serializable]
    public class LoginResponse
    {
        public string message;
    }


    public void LogIn()
    {
        StartCoroutine(SendLogInRequest(usernameInput.text, passwordInput.text));
    }

    public void SignUpBtn()
    {
        SceneManager.LoadScene("signup");
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

                    if (response.message == "Login successful")
                    {
                        Debug.Log("로그인 성공!!");
                        OnLoginSuccess(username);
                        SceneManager.LoadScene("lobby");
                    }
                    else if (response.message == "Invalid username" || response.message == "Invalid password")
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
                    Debug.LogError($"Error parsing JSON: {e.Message}");
                }
            }
            else
            {
                Debug.LogError($"SignIn failed: {request.error}");
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
               
                string savedNickname = PlayerPrefs.GetString("Nickname", "DefaultNickname");
                Debug.Log("현재 닉네임: " + savedNickname);
            }
            else
            {
                Debug.LogError($"GetLoginInfo failed: {request.error}");
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

}
