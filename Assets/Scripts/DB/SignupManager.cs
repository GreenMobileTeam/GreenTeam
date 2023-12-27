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

    private bool nullCheck;
    private bool pwCheck;

    public void SignUp()
    {
        if (ValidateInput())
        {
            StartCoroutine(SendSignUpRequest(usernameInput.text, passwordInput.text, nicknameInput.text));
        }
    }

    private bool NullCheck()
    {
        if (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(passwordInput.text) 
            || string.IsNullOrEmpty(passwordCheckInput.text)  || string.IsNullOrEmpty(nicknameInput.text))
        {
            Debug.LogError("모든 필드를 입력하세요.");
            return false;
        }
        return true;
    }

    private bool ValidateInput()
    {
        if (passwordInput.text != passwordCheckInput.text)
        {
            Debug.LogError("비밀번호가 일치하지 않습니다.");
            return false;
        }
        return true;
    }
    IEnumerator CheckDuplicate(string type, string value)
    {
        string url = $"{serverURL}/checkDuplicate/{type}/{value}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                ResponseData responseData = JsonUtility.FromJson<ResponseData>(request.downloadHandler.text);
                bool isDuplicate = responseData.isDuplicate;
                Debug.Log($"{type}이 중복됨: {isDuplicate}");
            }
            else
            {
                Debug.LogError($"중복 체크 실패: {request.error}");
            }
        }
    }

    public void CheckUserNameButton()
    {
        StartCoroutine(CheckDuplicate("username", usernameInput.text));
    }

    public void CheckNickNameButton()
    {
        StartCoroutine(CheckDuplicate("nickname", nicknameInput.text));
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

            nullCheck = NullCheck();
            pwCheck = ValidateInput();

            if(nullCheck)
            {
                if(pwCheck)
                {
                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        Debug.Log("회원가입 성공!");
                    }
                    else
                    {
                        Debug.LogError($"회원가입 실패: {request.error}");
                    }
                }
                else
                {
                    //Debug.Log("패스워드 불일치");
                }
            }
            else
            {
                //Debug.Log("칸이 비어있음");
            }
        }
    }
    public class ResponseData
    {
        public bool isDuplicate;
    }

}
