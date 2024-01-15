using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{
    public Character character;

    public GameObject[] ghostA;
    public GameObject[] ghostB;
    public GameObject[] ghostC;

    /*
    [SerializeField] private GameObject prefabGhostA;  //흰, 검, 파, 빨, 초, 노
    [SerializeField] private GameObject prefabGhostB;
    [SerializeField] private GameObject prefabGhostC;

    [SerializeField] private Material whiteMaterial;
    [SerializeField] private Material blackMaterial;
    [SerializeField] private Material blueMaterial;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material yellowMaterial;
    */

    [SerializeField] private float turnAngle = 30f; // 회전값

    private Quaternion initialRotationA;
    private Quaternion initialRotationB;
    private Quaternion initialRotationC;

    int nowColor = 0;
    string nowG;

    private void Start() // 초기 각도 저장
    {
        nowColor = nowColor = PlayerPrefs.GetInt("gColor");

        for (int i = 0; i < ghostA.Length; i++)
            initialRotationA = ghostA[i].transform.rotation;
        for (int i = 0; i < ghostB.Length; i++)
            initialRotationB = ghostB[i].transform.rotation;
        for (int i = 0; i < ghostC.Length; i++)
            initialRotationC = ghostC[i].transform.rotation;
        if (!PlayerPrefs.HasKey("gColor"))
        {
            nowColor = 0;
            nowG = "A";
        }

        else
        {
            int n = PlayerPrefs.GetInt("gColor");
            switch (n)
            {
                case 0:
                    ChangeColorWhite();
                    break;
                case 1:
                    ChangeColorBlack();
                    break;
                case 2:
                    ChangeColorBlue();
                    break;
                case 3:
                    ChangeColorRed();
                    break;
                case 4:
                    ChangeColorGreen();
                    break;
                case 5:
                    ChangeColorYellow();
                    break;
                default:
                    break;
            }

            string g = PlayerPrefs.GetString("nowGhost");

            switch (g)
            {
                case "A":
                    nowG = "GhostA";
                    ShowGhostA();
                    break;
                case "B":
                    nowG = "GhostB";
                    ShowGhostB();
                    break;
                case "C":
                    nowG = "GhostC";
                    ShowGhostC();
                    break;
            }
        }


    }

    // 플레이어 캐릭터 선택 (3종류)
    public void ShowGhostA()
    {
        PlayerPrefs.SetString("nowGhost","A");
        nowG = "A";
        for (int i = 0; i < ghostA.Length; i++)
        {
            ghostB[i].SetActive(false);
            ghostC[i].SetActive(false);
        }

        ghostA[nowColor].SetActive(true);

        ResetRotation(ghostA[nowColor], initialRotationA);
        CharacterManager.instance.currentCharacter = Character.ghostA;
    }
    public void ShowGhostB()
    {
        PlayerPrefs.SetString("nowGhost", "B");
        nowG = "B";
        for (int i = 0; i < ghostA.Length; i++)
        {
            ghostA[i].SetActive(false);
            ghostC[i].SetActive(false);
        }

        ghostB[nowColor].SetActive(true);

        ResetRotation(ghostB[nowColor], initialRotationB);
        CharacterManager.instance.currentCharacter = Character.ghostB;
    }
    public void ShowGhostC()
    {
        PlayerPrefs.SetString("nowGhost", "C");
        nowG = "C";
        for (int i = 0; i < ghostA.Length; i++)
        {
            ghostB[i].SetActive(false);
            ghostA[i].SetActive(false);
        }

        ghostC[nowColor].SetActive(true);

        ResetRotation(ghostC[nowColor], initialRotationC);
        CharacterManager.instance.currentCharacter = Character.ghostC;
    }

    // 좌우 회전
    public void TurnLeft()
    {
        RotateCharacter(turnAngle); 
    }
    public void TurnRight()
    {
        RotateCharacter(-turnAngle); 
    }

    // 색상 변경
    public void ChangeColorWhite()
    {
        PlayerPrefs.SetInt("gColor", 0);
        GameManager.instance.myGhostColor = "White";
        nowColor = 0;
        UpdateGhost();

    }
    public void ChangeColorBlack()
    {
        PlayerPrefs.SetInt("gColor", 1);
        GameManager.instance.myGhostColor = "Black";
        nowColor = 1;
        UpdateGhost();
    }
    public void ChangeColorBlue()
    {
        PlayerPrefs.SetInt("gColor", 2);
        GameManager.instance.myGhostColor = "Blue";
        nowColor = 2;
        UpdateGhost();
    }
    public void ChangeColorRed()
    {
        PlayerPrefs.SetInt("gColor", 3);
        GameManager.instance.myGhostColor = "Red";
        nowColor = 3;
        UpdateGhost();
    }
    public void ChangeColorGreen()
    {
        PlayerPrefs.SetInt("gColor", 4);
        GameManager.instance.myGhostColor = "Green";
        nowColor = 4;
        UpdateGhost();
    }
    public void ChangeColorYellow()
    {
        PlayerPrefs.SetInt("gColor", 5);
        GameManager.instance.myGhostColor = "Yellow";
        nowColor = 5;
        UpdateGhost();
    }

    private void UpdateGhost()
    {
        for (int i = 0; i < ghostA.Length; i++)
        {
            ghostA[i].SetActive(false);
            ghostB[i].SetActive(false);
            ghostC[i].SetActive(false);
        }

        if (nowG == "A")
        {
            ghostA[nowColor].SetActive(true);
        }
        if(nowG == "B")
        {
            ghostB[nowColor].SetActive(true);
        }
        if(nowG == "C")
        {
            ghostC[nowColor].SetActive(true);
        }
    }


    public void ChangeColor(GameObject player ,GameObject prefabPlayer, Material material) {
        //Debug.Log("Color Changed");
        Renderer playerRenderer = player.GetComponentInChildren<Renderer>();
        Renderer prefabPlayerRenderer = prefabPlayer.GetComponentInChildren<Renderer>();

        if (playerRenderer != null)
        {
            playerRenderer.material = material;
        }
        if (prefabPlayerRenderer != null)
        {
            prefabPlayerRenderer.material = material;
        }
    }

    private void RotateCharacter(float angle)
    {
        if (ghostA[nowColor].activeSelf)
        {
            ghostA[nowColor].transform.Rotate(Vector3.up, angle);
        }
        else if (ghostB[nowColor].activeSelf)
        {
            ghostB[nowColor].transform.Rotate(Vector3.up, angle);
        }
        else if (ghostC[nowColor].activeSelf)
        {
            ghostC[nowColor].transform.Rotate(Vector3.up, angle);
        }
    }

    private void ResetRotation(GameObject ghost, Quaternion initialRotation)
    {
        ghost.transform.rotation = initialRotation; // 저장된 초기 회전값으로 돌아감
    }
}

