using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnGhost : MonoBehaviour
{
    public GameObject[] charPrefabs;
    public GameObject playerGhost;

    void Start() // �κ񿡼� ������ ĳ���� SpawnPoint�� ����
    {
        playerGhost = Instantiate(charPrefabs[(int)CharacterManager.instance.currentCharacter]);
        playerGhost.transform.position = transform.position;
    }
}
