using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnGhost : MonoBehaviour
{
    public GameObject[] charPrefabs;
    public GameObject playerGhost;

    void Start() // 로비에서 선택한 캐릭터 SpawnPoint에 생성
    {
        playerGhost = Instantiate(charPrefabs[(int)CharacterManager.instance.currentCharacter]);
        playerGhost.transform.position = transform.position;
    }
}
