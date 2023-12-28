using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
    ghostA, ghostB, ghostC
}

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } 
        else if(instance != null) 
        {
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public Character currentCharacter;
}
