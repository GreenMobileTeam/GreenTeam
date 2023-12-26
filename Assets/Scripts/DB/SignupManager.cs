using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class SignupManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField passwordCheckInput;
    public TMP_InputField nicknameInput;

    private string serverURL = "localhost:3000";

    public void SignUp()
    {
        if (ValidateInput())
        {
            StartCoroutine(SendSignUpRequest(usernameInput.text, passwordInput.text, nicknameInput.text));
        }
    }

    public void CheckDuplicateUsername()
    {
        StartCoroutine(CheckDuplicateUsernameRequest(usernameInput.text));
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(passwordInput.text) || string.IsNullOrEmpty(nicknameInput.text))
        {
            Debug.LogError("모든 필드를 입력하세요.");
            return false;
        }

        if (passwordInput.text != passwordCheckInput.text)
        {
            Debug.LogError("비밀번호가 일치하지 않습니다.");
            return false;
        }

        return true;
    }

    IEnumerator CheckDuplicateUsernameRequest(string username)
    {
        string url = $"{serverURL}/check-username/{username}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                if (request.downloadHandler.text == "true")
                {
                    Debug.LogError("이미 사용 중인 아이디입니다.");
                }
                else
                {
                    Debug.Log("사용 가능한 아이디입니다.");
                }
            }
            else
            {
                Debug.LogError($"서버 요청 실패: {request.error}");
            }
        }
    }

    IEnumerator SendSignUpRequest(string username, string password, string nickname)
    {
        string url = $"{serverURL}/signup";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("nickname", nickname);

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("회원가입 성공!");
            }
            else
            {
                Debug.LogError($"회원가입 실패: {request.error}");
            }
        }
    }
}
