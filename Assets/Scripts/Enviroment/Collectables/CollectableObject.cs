using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour, ICollectable
{
    public ICollectable.CollectableType Type { get; set; }
    public float slowDown = 0.2f;
}