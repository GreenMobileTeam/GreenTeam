using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class SessionManager : MonoBehaviour
{
    public static SessionManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ...

    private IEnumerator CheckSessionStatus()
    {
        while (true)
        {
            yield return new WaitForSeconds(1); // 60�ʸ��� ���� ���� Ȯ��

            // ���� ���� Ȯ���� ���� ��û ������
            StartCoroutine(SendCheckSessionRequest());
        }
    }

    private IEnumerator SendCheckSessionRequest()
    {
        string url = "http://localhost:3000/checkSession"; // ���� ���� Ȯ���� ���� ���� URL

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // ���� ������ �޾� ó��
                string jsonResponse = request.downloadHandler.text;
                CheckSessionResponse response = JsonUtility.FromJson<CheckSessionResponse>(jsonResponse);

                if (response.isLoggedIn)
                {
                    Debug.Log("���� ��ȿ. ���� �����: " + response.username);
                }
                else
                {
                    Debug.Log("���� ����. �α׾ƿ� ó�� ���� �����ϼ���.");

                    LogOut();
                }
            }
            else if (request.responseCode == 401)
            {
                Debug.Log("���� ����. �α׾ƿ� ó�� ���� �����ϼ���.");
                LogOut();
            }
            else
            {
                Debug.LogError("���� Ȯ�� ��û ����: " + request.error);
            }
        }
    }

    public class CheckSessionResponse
    {
        public bool isLoggedIn;
        public string username;
    }

    private void Start()
    {
        StartCoroutine(CheckSessionStatus());
    }


    public void LogOut()
    {
        if (SceneManager.GetActiveScene().name == "Lobby_A" && PlayerPrefs.GetInt("IsGuest", 0) == 0)
        {
            StartCoroutine(SendLogOutRequest());
        }
    }

    IEnumerator SendLogOutRequest()
    {
        string url = "http://localhost:3000/logout";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("���� �� �α׾ƿ� ����");
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene("login");
            }
            else
            {
                Debug.LogError("���� �� �α׾ƿ� ����: " + request.error);
            }
        }
    }

}