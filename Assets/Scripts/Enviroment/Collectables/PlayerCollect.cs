using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player interaction with ICollectable.
/// </summary>
public class PlayerCollect : MonoBehaviour, ICollect
{
    private ICollectable _collectableObject;

    public void Take()
    {
        throw new System.NotImplementedException();
    }

    public void Drop()
    {
        throw new System.NotImplementedException();
    }
}
