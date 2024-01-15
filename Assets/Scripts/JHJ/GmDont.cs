using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GmDont : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
