using System.Collections.Generic;
using UnityEngine;

public class CollectingZone : MonoBehaviour
{
    private Dictionary<ICollectable.CollectableType, int> collectablesCounter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ICollectable>() is not null)
            collectablesCounter[other.GetComponent<ICollectable>().Type] += 1;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ICollectable>() is not null)
            collectablesCounter[other.GetComponent<ICollectable>().Type] -= 1;
    }
}