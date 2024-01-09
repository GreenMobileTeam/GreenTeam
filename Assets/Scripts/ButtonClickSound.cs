using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    public AudioClip clickSound;  // �Ҹ� ������ ������ ����
    private AudioSource audioSource;  // AudioSource ������Ʈ�� ������ ����

    void Start()
    {
        // AudioSource ������Ʈ�� ���� ���� ������Ʈ�� �߰�
        audioSource = gameObject.AddComponent<AudioSource>();
        // Ŭ�� ���带 ����
        audioSource.clip = clickSound;

        // Button�� Ŭ�� �̺�Ʈ�� �߰�
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(PlayClickSound);
        }
    }

    void PlayClickSound()
    {
        // Ŭ�� ���带 ���
        audioSource.Play();
    }
}
