using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.Collections;

public class MainController : MonoBehaviour
{
    public TextMeshProUGUI text;
    bool next = false;

    private void Awake()
    {
        StartCoroutine(FadeTextToZero());
    }

    public IEnumerator FadeTextToFullAlpha() // ���İ� 0���� 1�� ��ȯ
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        }
        StartCoroutine(FadeTextToZero());
    }

    public IEnumerator FadeTextToZero()  // ���İ� 1���� 0���� ��ȯ
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }
        StartCoroutine(FadeTextToFullAlpha());
    }

    public void Touched()
    {
        next = true;
        SceneManager.LoadScene("login");
    }
}
