using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //[Header("Scripts")]

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
}
