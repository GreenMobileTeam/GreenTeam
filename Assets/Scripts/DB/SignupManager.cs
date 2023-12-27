using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SignupManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField passwordCheckInput;
    public TMP_InputField nicknameInput;

    public Button signupButn;

    private string serverURL = "https://soft-actors-shine.loca.lt";

    private bool nullCheck;
    private bool pwCheck;

    public bool nickCheck;
    public bool userCheck;

    private void Awake()
    {
        nickCheck = true;
        userCheck = true;
        pwCheck = false;
        nullCheck = false;
    }

    private void Update()
    {
        if(nullCheck = NullCheck() && !nickCheck && !userCheck)
        {
            signupButn.interactable = true;
        }
        else
        {
            signupButn.interactable = false;
        }
    }

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
            return false;
        }
        return true;
    }

    private bool ValidateInput()
    {
        if (passwordInput.text != passwordCheckInput.text)
        {
            Debug.LogError("비밀번호가 일치하지 않습니다.");
            passwordInput.text = "";
            passwordCheckInput.text = "";
            return false;
        }
        return true;
    }
    IEnumerator CheckDuplicate(string type, TMP_InputField value)
    {
        string url = $"{serverURL}/checkDuplicate/{type}/{value.text}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                ResponseData responseData = JsonUtility.FromJson<ResponseData>(request.downloadHandler.text);
                bool isDuplicate = responseData.isDuplicate;
                Debug.Log($"{type}이 중복됨: {isDuplicate}");

                if (type == "username")
                {
                    userCheck = isDuplicate;
                }
                else if (type == "nickname")
                {
                    nickCheck = isDuplicate;
                }

                if (isDuplicate)
                {
                    value.text = "";
                }
                else
                {

                }
            }
            else
            {
                Debug.LogError($"중복 체크 실패: {request.error}");
            }
        }
    }

    public void CheckUserNameButton()
    {
        StartCoroutine(CheckDuplicate("username", usernameInput));
    }

    public void CheckNickNameButton()
    {
        StartCoroutine(CheckDuplicate("nickname", nicknameInput));
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

            pwCheck = ValidateInput();
          
            if (pwCheck)
            {
                if (request.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("회원가입 성공!");
                    ReturnLoginScene();
                }
                else
                {
                    Debug.LogError($"회원가입 실패: {request.error}");
                }
            }
        }
    }
    
    public class ResponseData
    {
        public bool isDuplicate;
    }

    public void ReturnLoginScene()
    {
        SceneManager.LoadScene("login");
    }

}
