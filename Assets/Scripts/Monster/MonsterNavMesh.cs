using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public sealed class MonsterNavMesh : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private int _startWaypoint;
    [SerializeField, Min(0f)] private float _speed = 2f;
    [SerializeField, Min(0f)] private float _waitTime = 1f;

    private int _destinationWaypoint;

    private bool _isWaiting;

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
        _agent.autoBraking = false;
        GotoNextPoint();
    }

    private void Update()
    {
        if (!_isWaiting)
        {
            if (!_agent.pathPending && _agent.remainingDistance < 0.1f)
            {
                _isWaiting = true;
                StartCoroutine(WaitAtWaypoint());
            }
        }
    }

    private void GotoNextPoint()
    {
        if (_waypoints.Length == 0)
        {
            Debug.LogError("Zero waypoints!");
            return;
        }

        _agent.destination = _waypoints[_destinationWaypoint].position;
        _destinationWaypoint = (_destinationWaypoint + 1) % _waypoints.Length;
    }

    private IEnumerator WaitAtWaypoint()
    {
        yield return new WaitForSeconds(_waitTime);
        _isWaiting = false;
        GotoNextPoint();
        yield break;
    }
}
