using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class CollectableObject : MonoBehaviour, ICollectable
{
    public CollectableType Type => _type;
    public float Slowdown => _slowdown;
    public bool InZone = false;
    public bool saidLine = false;

    [SerializeField] private CollectableType _type;
    [SerializeField] private float _slowdown;

    public enum CollectableType
    {
        Any,
        //Food,
        //Scheme
    }
}