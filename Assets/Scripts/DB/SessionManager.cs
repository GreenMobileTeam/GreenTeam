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
            yield return new WaitForSeconds(2); // 60초마다 세션 상태 확인

            // 세션 상태 확인을 위한 요청 보내기
            StartCoroutine(SendCheckSessionRequest());
        }
    }

    private IEnumerator SendCheckSessionRequest()
    {
        string url = "http://localhost:3000/checkSession"; // 세션 상태 확인을 위한 서버 URL

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // 서버 응답을 받아 처리
                string jsonResponse = request.downloadHandler.text;
                CheckSessionResponse response = JsonUtility.FromJson<CheckSessionResponse>(jsonResponse);

                if (response.isLoggedIn)
                {
                    // 세션이 유효한 경우
                    Debug.Log("세션 유효. 현재 사용자: " + response.username);
                }
                else
                {
                    // 세션이 만료된 경우
                    Debug.Log("세션 만료. 로그아웃 처리 등을 수행하세요.");

                    // 클라이언트에서의 로그아웃 처리 (세션 정보 초기화 등)
                    LogOut();
                }
            }
            else if (request.responseCode == 401)
            {
                // 401 상태 코드인 경우 (세션 만료)
                Debug.Log("세션 만료. 로그아웃 처리 등을 수행하세요.");

                // 클라이언트에서의 로그아웃 처리 (세션 정보 초기화 등)
                LogOut();
            }
            else
            {
                // 서버 통신 오류 처리
                Debug.LogError("세션 확인 요청 실패: " + request.error);
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
                Debug.Log("서버 측 로그아웃 성공");
                PlayerPrefs.SetInt("IsGuest", 0);
            }
            else
            {
                Debug.LogError("서버 측 로그아웃 실패: " + request.error);
            }
        }
    }
}