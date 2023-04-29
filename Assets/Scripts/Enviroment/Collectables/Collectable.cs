using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, ICollectable
{
    public CollectableType Type;
    public float slowDown = 0.2f;
    public enum CollectableType
    {
        Food,
        Scheme
    }    
}