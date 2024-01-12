using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SessionChecker : MonoBehaviour
{
    string serverURL = "http://greenacademi.store";

    public float seconds;

    private static SessionChecker instance = null;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Main")
        {
            Destroy(this.gameObject);
        }
    }

    public static SessionChecker Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void Start()
    {
        StartCoroutine(CheckSessionPeriodically());
    }

    IEnumerator CheckSessionPeriodically()
    {
        WaitForSeconds delay = new WaitForSeconds(seconds); 

        while (GameManager.instance.myID != "" && !GameManager.instance.isGuest)
        {
            yield return delay;
            StartCoroutine(CheckSession());
        }
    }

    IEnumerator CheckSession()
    {
        string url = $"{serverURL}/checkSession";
        WWWForm form = new WWWForm();
        form.AddField("username", GameManager.instance.myID);

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success || request.responseCode == 401)
            {
                string jsonResponse = request.downloadHandler.text;

                SessionResponse response = JsonUtility.FromJson<SessionResponse>(jsonResponse);

                if (response.message == "login")
                {
                }
                else if (response.message == "auto logout")
                {
                    Debug.Log("�ٸ� ������ �α���");
                    SceneManager.LoadScene("Main");
                }
            }
            else
            {
                Debug.LogError("���� ����: " + request.error);
            }
        }
    }

    public class SessionResponse
    {
        public string message;
    }

}
