using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;


public class Login : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    private string serverURL = "localhost:3000"; // 서버 URL로 교체

    public void SignUp()
    {
        StartCoroutine(SendSignUpRequest(usernameInput.text, passwordInput.text));
    }

    public void SignIn()
    {
        StartCoroutine(SendSignInRequest(usernameInput.text, passwordInput.text));
    }

    IEnumerator SendSignUpRequest(string username, string password)
    {
        string url = $"{serverURL}/signup";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("SignUp successful!");
            }
            else
            {
                Debug.LogError($"SignUp failed: {request.error}");
            }
        }
    }

    IEnumerator SendSignInRequest(string username, string password)
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
                Debug.Log("SignIn successful!");
            }
            else
            {
                Debug.LogError($"SignIn failed: {request.error}");
            }
        }
    }
}
