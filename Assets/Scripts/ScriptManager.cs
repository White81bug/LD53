using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    public static ScriptManager Instance;
    [Header("Scripts")]
    public TransformParentManager TransformParentManager;
    public PlayerCollect PlayerCollect;
    public InputManager InputManager;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
}
