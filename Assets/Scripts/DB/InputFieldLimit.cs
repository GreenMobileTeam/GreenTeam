using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldLimit : MonoBehaviour
{
    private TMP_InputField inputField;
    public int maxCharacterLimit;

    // Start is called before the first frame update
    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    void Start()
    {
        inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
    }

    void OnInputFieldValueChanged(string newText)
    {
        if (newText.Length > maxCharacterLimit)
        {
            inputField.text = newText.Substring(0, maxCharacterLimit);
        }
    }
}
