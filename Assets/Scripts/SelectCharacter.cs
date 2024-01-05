using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{
    public Character character;

    [SerializeField] private GameObject ghostA;
    [SerializeField] private GameObject ghostB;
    [SerializeField] private GameObject ghostC;

    [SerializeField] private GameObject prefabGhostA;
    [SerializeField] private GameObject prefabGhostB;
    [SerializeField] private GameObject prefabGhostC;

    [SerializeField] private Material whiteMaterial;
    [SerializeField] private Material blackMaterial;
    [SerializeField] private Material blueMaterial;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material yellowMaterial;

    [SerializeField] private float turnAngle = 30f; // 회전값

    private Quaternion initialRotationA;
    private Quaternion initialRotationB;
    private Quaternion initialRotationC;

    private void Start() // 초기 각도 저장
    {
        initialRotationA = ghostA.transform.rotation;
        initialRotationB = ghostB.transform.rotation;
        initialRotationC = ghostC.transform.rotation;
        if(!PlayerPrefs.HasKey("gColor"))
            ChangeColorWhite();
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
                    ShowGhostA();
                    break;
                case "B":
                    ShowGhostB();
                    break;
                case "C":
                    ShowGhostC();
                    break;
            }
        }


    }

    // 플레이어 캐릭터 선택 (3종류)
    public void ShowGhostA()
    {
        PlayerPrefs.SetString("nowGhost","A");
        ghostA.SetActive(true);
        ghostB.SetActive(false);
        ghostC.SetActive(false);
        ResetRotation(ghostA, initialRotationA);
        CharacterManager.instance.currentCharacter = Character.ghostA;
    }
    public void ShowGhostB()
    {
        PlayerPrefs.SetString("nowGhost", "B");
        ghostA.SetActive(false);
        ghostB.SetActive(true);
        ghostC.SetActive(false);
        ResetRotation(ghostB, initialRotationB);
        CharacterManager.instance.currentCharacter = Character.ghostB;
    }
    public void ShowGhostC()
    {
        PlayerPrefs.SetString("nowGhost", "C");
        ghostA.SetActive(false);
        ghostB.SetActive(false);
        ghostC.SetActive(true);
        ResetRotation(ghostC, initialRotationC);
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
        ChangeColor(ghostA, prefabGhostA, whiteMaterial);
        ChangeColor(ghostB, prefabGhostB, whiteMaterial);
        ChangeColor(ghostC, prefabGhostC, whiteMaterial);
    }
    public void ChangeColorBlack()
    {
        PlayerPrefs.SetInt("gColor", 1);
        ChangeColor(ghostA, prefabGhostA, blackMaterial);
        ChangeColor(ghostB, prefabGhostB, blackMaterial);
        ChangeColor(ghostC, prefabGhostC, blackMaterial);
    }
    public void ChangeColorBlue()
    {
        PlayerPrefs.SetInt("gColor", 2);
        ChangeColor(ghostA, prefabGhostA, blueMaterial);
        ChangeColor(ghostB, prefabGhostB, blueMaterial);
        ChangeColor(ghostC, prefabGhostC, blueMaterial);
    }
    public void ChangeColorRed()
    {
        PlayerPrefs.SetInt("gColor", 3);
        ChangeColor(ghostA, prefabGhostA, redMaterial);
        ChangeColor(ghostB, prefabGhostB, redMaterial);
        ChangeColor(ghostC, prefabGhostC, redMaterial);
    }
    public void ChangeColorGreen()
    {
        PlayerPrefs.SetInt("gColor", 4);
        ChangeColor(ghostA, prefabGhostA, greenMaterial);
        ChangeColor(ghostB, prefabGhostB, greenMaterial);
        ChangeColor(ghostC, prefabGhostC, greenMaterial);
    }
    public void ChangeColorYellow()
    {
        PlayerPrefs.SetInt("gColor", 5);
        ChangeColor(ghostA, prefabGhostA, yellowMaterial);
        ChangeColor(ghostB, prefabGhostB, yellowMaterial);
        ChangeColor(ghostC, prefabGhostC, yellowMaterial);
    }

    public void ChangeColor(GameObject player ,GameObject prefabPlayer, Material material) {
        Debug.Log("Color Changed");
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
        if (ghostA.activeSelf)
        {
            ghostA.transform.Rotate(Vector3.up, angle);
        }
        else if (ghostB.activeSelf)
        {
            ghostB.transform.Rotate(Vector3.up, angle);
        }
        else if (ghostC.activeSelf)
        {
            ghostC.transform.Rotate(Vector3.up, angle);
        }
    }

    private void ResetRotation(GameObject ghost, Quaternion initialRotation)
    {
        ghost.transform.rotation = initialRotation; // 저장된 초기 회전값으로 돌아감
    }
}

