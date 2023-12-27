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
            Debug.LogError("��� �ʵ带 �Է��ϼ���.");
            return false;
        }
        return true;
    }

    private bool ValidateInput()
    {
        if (passwordInput.text != passwordCheckInput.text)
        {
            Debug.LogError("��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
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
                Debug.Log($"{type}�� �ߺ���: {isDuplicate}");
            }
            else
            {
                Debug.LogError($"�ߺ� üũ ����: {request.error}");
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
                        Debug.Log("ȸ������ ����!");
                    }
                    else
                    {
                        Debug.LogError($"ȸ������ ����: {request.error}");
                    }
                }
                else
                {
                    //Debug.Log("�н����� ����ġ");
                }
            }
            else
            {
                //Debug.Log("ĭ�� �������");
            }
        }
    }
    public class ResponseData
    {
        public bool isDuplicate;
    }

}
