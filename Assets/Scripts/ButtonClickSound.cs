using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    public AudioClip clickSound;  // 소리 파일을 저장할 변수
    private AudioSource audioSource;  // AudioSource 컴포넌트를 저장할 변수

    void Start()
    {
        // AudioSource 컴포넌트를 현재 게임 오브젝트에 추가
        audioSource = gameObject.AddComponent<AudioSource>();
        // 클릭 사운드를 설정
        audioSource.clip = clickSound;

        // Button에 클릭 이벤트를 추가
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(PlayClickSound);
        }
    }

    void PlayClickSound()
    {
        // 클릭 사운드를 재생
        audioSource.Play();
    }
}
