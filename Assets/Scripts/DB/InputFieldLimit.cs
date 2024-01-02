using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class InputFieldLimit : MonoBehaviour
{
    private TMP_InputField inputField;

    // Start is called before the first frame update
    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    void Start()
    {
        inputField.onValueChanged.AddListener(OnInputFieldValueLimit);
    }

    void OnInputFieldValueLimit(string newText)
    {
        if(gameObject.name != "Nickname")
            inputField.text = Regex.Replace(newText, @"[^0-9a-zA-Z°¡-ÆR¤¡-¤¾¤¿-¤Ó!@#$%^&*()_+]", "");
        else
            inputField.text = Regex.Replace(newText, @"[^0-9a-zA-Z°¡-ÆR¤¡-¤¾¤¿-¤Ó]", "");
    }

    public void OnInputKoreanLimit()
    {
            if (inputField.isFocused)
            {
                Input.imeCompositionMode = IMECompositionMode.Off;
            }
            else
            {
                Input.imeCompositionMode = IMECompositionMode.Auto;
            }
        }
}
