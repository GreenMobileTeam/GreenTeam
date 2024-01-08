using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserNameChanged : MonoBehaviour
{
    private TMP_InputField inputField;

    // Start is called before the first frame update
    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
