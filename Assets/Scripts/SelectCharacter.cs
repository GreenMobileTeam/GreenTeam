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

    [SerializeField] private float turnAngle = 10f; // ȸ����

    private Quaternion initialRotationA;
    private Quaternion initialRotationB;
    private Quaternion initialRotationC;

    private void Start() // �ʱ� ���� ����
    {
        initialRotationA = ghostA.transform.rotation;
        initialRotationB = ghostB.transform.rotation;
        initialRotationC = ghostC.transform.rotation;
    }

    public void ShowGhostA()
    {
        ghostA.SetActive(true);
        ghostB.SetActive(false);
        ghostC.SetActive(false);
        ResetRotation(ghostA, initialRotationA);
        CharacterManager.instance.currentCharacter = Character.ghostA;
    }

    public void ShowGhostB()
    {
        ghostA.SetActive(false);
        ghostB.SetActive(true);
        ghostC.SetActive(false);
        ResetRotation(ghostB, initialRotationB);
        CharacterManager.instance.currentCharacter = Character.ghostB;
    }

    public void ShowGhostC()
    {
        ghostA.SetActive(false);
        ghostB.SetActive(false);
        ghostC.SetActive(true);
        ResetRotation(ghostC, initialRotationC);
        CharacterManager.instance.currentCharacter = Character.ghostC;
    }

    public void TurnLeft() // �������� ȸ��
    {
        RotateCharacter(turnAngle); 
    }

    public void TurnRight() // ���������� ȸ��
    {
        RotateCharacter(-turnAngle); 
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Map_A");
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
        ghost.transform.rotation = initialRotation; // ����� �ʱ� ȸ�������� ���ư�
    }
}

