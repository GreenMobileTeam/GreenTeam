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

    public TMP_Text usernameCheck;
    public TMP_Text nicknameCheck;
    public TMP_Text passwordCheck;

    public Button signupButn;

    public GameObject Checkpopup;
    public TMP_Text checkText;

    string serverURL = "http://greenacademi.store";

    private bool nullCheck;
    private bool pwCheck;
    public bool nickCheck;
    public bool userCheck;
    public bool wordCheck;

    private void Awake()
    {
        nickCheck = true;
        userCheck = true;
        wordCheck = true;
        pwCheck = false;
        nullCheck = false;
    }

    private void Update()
    {
        pwCheck = ValidateInput();
        Debug.Log("�г��� üũ : " + nickCheck);
        Debug.Log("�г��� üũ : " + userCheck);

        if (!nickCheck && !wordCheck)
        {
            nicknameCheck.text = "o";
            nicknameCheck.color = Color.green;
        }
        else
        {
            nicknameCheck.text = "x";
            nicknameCheck.color = Color.red;
        }

        if (!userCheck)
        {
            usernameCheck.text = "o";
            usernameCheck.color = Color.green;
        }
        else
        {
            usernameCheck.text = "x";
            usernameCheck.color = Color.red;
        }

        if (nullCheck = NullCheck() && !nickCheck && !userCheck && !wordCheck)
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
        if (passwordInput.text == "" && passwordCheckInput.text == "")
            return false;

        if (passwordInput.text != passwordCheckInput.text)
        {
            passwordCheck.text = "x";
            passwordCheck.color = Color.red;
            return false;
        }

        passwordCheck.text = "o";
        passwordCheck.color = Color.green;
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
                Debug.Log(type + "�ߺ� üũ" + isDuplicate);

                if (type == "username")
                {
                    if (isDuplicate)
                    {
                        checkText.text = "�̹� �����ϴ� ���̵��Դϴ�.";
                        Checkpopup.SetActive(true);
                        userCheck = isDuplicate;
                    }
                    else
                    {
                        userCheck = isDuplicate;
                    }
                }
                else if (type == "nickname")
                {
                    if (isDuplicate)
                    {
                        checkText.text = "�̹� �����ϴ� �г����Դϴ�.";
                        Checkpopup.SetActive(true);
                        nickCheck = isDuplicate;
                    }
                    else
                    {
                        StartCoroutine(CheckCensorship("nickname", value));
                    }
                }
            }
            else
            {
                Debug.LogError(" �ߺ� üũ ���� " + request.error);
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

    IEnumerator CheckCensorship(string type, TMP_InputField value)
    {
        string url = $"{serverURL}/checkCensorship/{type}/{value.text}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                ResponseData responseData = JsonUtility.FromJson<ResponseData>(request.downloadHandler.text);
                bool isCensored = responseData.isCensored;  
                Debug.Log(type + "��Ӿ� ���� : " + isCensored);

                if (isCensored)
                {
                    checkText.text = "��� �Ұ����� �ܾ\n���ԵǾ� �ֽ��ϴ�.";
                    Checkpopup.SetActive(true);
                    nicknameInput.text = "";
                    wordCheck = true;
                    nickCheck = false;
                }
                else
                {
                    wordCheck = false;
                    nickCheck = false;
                }
            }
            else
            {
                Debug.LogError("��Ӿ� üũ ���� :" + request.error);
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
                      
            if (pwCheck)
            {
                if (request.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("ȸ������ ����");
                    ReturnLoginScene();
                }
                else
                {
                    Debug.LogError("ȸ������ ���� : " + request.error);
                }
            }
        }
    }
    
    public class ResponseData
    {
        public bool isDuplicate;
        public bool isCensored;
    }

    public void ReturnLoginScene()
    {
        SceneManager.LoadScene("login");
    }

    public void OnUsernameValueChanged()
    {
        if(!userCheck)
        {
            userCheck = true;
        }
    }

    public void OnNicknameValueChanged()
    {
        if (!nickCheck)
        {
            nickCheck = true;
            wordCheck = true;
        }
    }

    public void PopUpClose()
    {
        Checkpopup.SetActive(false);
    }

}
