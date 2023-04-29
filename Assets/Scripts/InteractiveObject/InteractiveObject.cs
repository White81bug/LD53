using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractiveObject : MonoBehaviour
{
    [Header("Physics")]
    //private Rigidbody _rb;
    private Collider _collider;
    private bool _inTrigger;

    [Header("Renderer")]
    private Material _material;
    private Color _startColor;

    protected virtual void Awake()
    {
        //_rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        _material = GetComponent<Renderer>().material;
        _startColor = _material.color;
    }

    protected virtual void OnMouseEnter()
    {
        Debug.Log("MouseEnter");
        _material.color = Color.yellow;
    }

    protected virtual void OnMouseExit()
    {
        Debug.Log("MouseExit");
        _material.color = _startColor;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        _inTrigger = true;
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        _inTrigger = false;
    }
}
