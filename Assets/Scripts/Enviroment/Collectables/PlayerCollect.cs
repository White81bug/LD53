using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player interaction with ICollectable.
/// </summary>
[RequireComponent(typeof(Collider))]
public class PlayerCollect : MonoBehaviour
{
    [SerializeField] private Transform _pointOfObject;

    private List<GameObject> _collectableObjects; // List<> to prevent bug with several GameObjects collision.
    private GameObject _currentObject;

    private void Awake()
    {
        _collectableObjects = new List<GameObject>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Take();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CollectableObject>())
        {
            if (!_collectableObjects.Contains(other.gameObject))
            {
                _collectableObjects.Add(other.gameObject);
                Debug.Log("Added");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CollectableObject>())
        {
            _collectableObjects.Remove(other.gameObject);
            Debug.Log("Removed");
        }
    }

    private void Take()
    {
        if (_currentObject is null && _collectableObjects.Count >= 1)
        {
            _currentObject = AdditionalMath.FindClosestGameObject(transform, _collectableObjects);
            _currentObject.transform.position = _pointOfObject.transform.position;
            _currentObject.transform.SetParent(_pointOfObject);
        }
    }

    private void Drop()
    {
        if (_currentObject is not null)
        {
            RaycastHit hit;
            if (Physics.Raycast(_currentObject.transform.position, Vector3.down, out hit))
                _currentObject.transform.position = hit.point;
            else Debug.LogError("Physics.Raycast didn't found floor.");

            _currentObject.transform.SetParent(null);
            _currentObject = null;
        }
    }
}
