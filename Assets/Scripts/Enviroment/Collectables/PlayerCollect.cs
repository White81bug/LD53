using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Player interaction with ICollectable.
/// </summary>
[RequireComponent(typeof(Collider))]
public class PlayerCollect : MonoBehaviour
{
    private List<GameObject> _collectableObjects = new List<GameObject>(); // List<> to prevent bug with several GameObjects collision.

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CollectableObject>())
        {
            _collectableObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CollectableObject>())
        {
            _collectableObjects.Remove(other.gameObject);
        }
    }

    private void Take()
    {
        throw new System.NotImplementedException();
    }

    private void Drop()
    {
        throw new System.NotImplementedException();
    }
}
