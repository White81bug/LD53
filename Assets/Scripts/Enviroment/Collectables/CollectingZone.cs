using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class CollectingZone : MonoBehaviour
{
    private Dictionary<CollectableObject.CollectableType, int> _collectablesCounter;

    private List<Collider> _colliders;

    private void Awake()
    {
        _collectablesCounter = new Dictionary<CollectableObject.CollectableType, int>();
        _collectablesCounter.Add(CollectableObject.CollectableType.Any, 0);
        //_collectablesCounter.Add(CollectableObject.CollectableType.Scheme, 0);
        //_collectablesCounter.Add(CollectableObject.CollectableType.Food, 0);

        _colliders = new List<Collider>();
    }

    private void OnEnable()
    {
        ScriptManager.Instance.PlayerCollect.OnDropObject.AddListener(CheckDrop);
        ScriptManager.Instance.PlayerCollect.OnTakeObject.AddListener(CheckTake);
    }

    private void OnDisable()
    {
        ScriptManager.Instance.PlayerCollect.OnDropObject.RemoveListener(CheckDrop);
        ScriptManager.Instance.PlayerCollect.OnTakeObject.RemoveListener(CheckTake);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("In");
        if (other.GetComponent<CollectableObject>())
        {
            if (!_colliders.Contains(other))
            {
                _colliders.Add(other);
            }
            // It doesn't means, that Player dropped CollectableObject!
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Out");
        if (other.GetComponent<CollectableObject>())
        {
            _colliders.Remove(other);
            // It doesn't means, that Player took CollectableObject! He can join with CollectableObject in hands, didn't drop and go out.

            //if (other.GetComponent<ICollectable>() is not null)
            //    _collectablesCounter[other.GetComponent<CollectableObject>().Type] -= 1;
        }
    }

    /// <summary>
    /// Invokes when Player took CollectableObject to check took from CollectingZone or not.
    /// </summary>
    private void CheckTake(GameObject gameObject)
    {
        if (_colliders.Contains(gameObject.GetComponent<Collider>()))
        {
            _collectablesCounter[gameObject.GetComponent<CollectableObject>().Type] -= 1;
        }
        //Debug.Log($"Food - {_collectablesCounter[CollectableObject.CollectableType.Food]}, Scheme -  {_collectablesCounter[CollectableObject.CollectableType.Scheme]}");
    }

    /// <summary>
    /// Invokes when Player dropped CollectableObject to check dropped on CollectingZone or not.
    /// </summary>
    private void CheckDrop(GameObject gameObject)
    {
        if (_colliders.Contains(gameObject.GetComponent<Collider>()))
        {
            //_collectablesCounter[gameObject.GetComponent<CollectableObject>().Type] += 1;
            gameObject.GetComponent<QuestTrigger>().AdvanceQuest();
            gameObject.GetComponent<CollectableObject>().InZone = true;
        }
        //Debug.Log($"Food - {_collectablesCounter[CollectableObject.CollectableType.Food]}, Scheme -  {_collectablesCounter[CollectableObject.CollectableType.Scheme]}");
    }
}