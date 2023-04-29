using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollectableObject : MonoBehaviour, ICollectable
{
    public CollectableType Type => _type;
    public float Slowdown => _slowdown;

    [SerializeField] private CollectableType _type;
    [SerializeField] private float _slowdown;

    public enum CollectableType
    {
        Food,
        Scheme
    }
}