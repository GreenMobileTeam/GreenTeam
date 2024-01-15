using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginBtn : MonoBehaviour
{
    public void SignUpBtn()
    {
        SceneManager.LoadScene("signup");
    }
    public void GuestLogin()
    {
        SceneManager.LoadScene("Lobby_A");
        PlayerPrefs.SetInt("IsGuest", 1);
    }

    public void ExitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
