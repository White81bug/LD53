using System.Collections;
using UnityEngine;

public sealed class Monster : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private int _startWaypoint;
    [SerializeField, Min(0f)] private float _speed = 2f;
    [SerializeField, Min(0f)] private float _waitTime = 1f;

    private int _currentWaypoint;

    private bool _isWaiting;

    private void Awake()
    {
        _currentWaypoint = _startWaypoint;
        transform.position = _waypoints[_currentWaypoint].position;
        //AdditionalMath.FindClosestTransform(transform, _waypoints); Finding closest point.
    }

    private void Update()
    {
        if (!_isWaiting)
        {
            transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _waypoints[_currentWaypoint].position) < 0.1f)
            {
                _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;
                _isWaiting = true;
                StartCoroutine(WaitAtWaypoint());
            }
        }
    }

    private IEnumerator WaitAtWaypoint()
    {
        yield return new WaitForSeconds(_waitTime);
        _isWaiting = false;
        yield break;
    }
}