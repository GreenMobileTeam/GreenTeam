using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{

    // Start is called before the first frame update
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    private string serverURL = "localhost:3000"; // 서버 URL로 교체해야됨 

    public void SignIn()
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

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("로그인 성공!!");
                SceneManager.LoadScene("lobby");
            }
            else
            {
                Debug.LogError($"SignIn failed: {request.error}");
            }
        }
    }

}
