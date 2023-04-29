using System.Collections.Generic;
using UnityEngine;

public class CollectingZone : MonoBehaviour
{
    private Dictionary<Collectable.CollectableType, int> collectablesCounter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collectable>() is not null)
            collectablesCounter[other.GetComponent<Collectable>().Type] += 1;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collectable>() is not null)
            collectablesCounter[other.GetComponent<Collectable>().Type] -= 1;
    }
}