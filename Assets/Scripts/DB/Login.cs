using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;


public class Login : MonoBehaviour
{
    // Start is called before the first frame update
    public enum LoginStatus
    {
        Login
    }

    private readonly Button btnLogin;
    private LoginStatus btnStatus;
    public Login(Button btnLogin)
    {
        this.btnLogin = btnLogin;
    }

    public LoginStatus GetBtnStatus()
    {
        return btnStatus;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
