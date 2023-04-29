using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent), typeof(SphereCollider))]
public class MonsterChasing : MonoBehaviour
{
    public UnityAction OnStartChasing;
    public UnityAction OnStopChasing;

    [SerializeField, Min(0f)] private float _radiusOfDetecting;
    [SerializeField, Min(0f)] private float _speed = 2f;

    private NavMeshAgent _agent;

    private GameObject _player;
    private bool _isChasing;

    private SphereCollider _sphereCollider;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiusOfDetecting);
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
        _agent.autoBraking = false;

        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = _radiusOfDetecting;
        _sphereCollider.isTrigger = true;
    }

    private void Update()
    {
        if (_isChasing)
        {
            _agent.destination = _player.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MovementController>())
        {
            _player = other.gameObject;
            _isChasing = true;
            OnStartChasing?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<MovementController>())
        {
            _player = null;
            _isChasing = false;
            OnStopChasing?.Invoke();
        }
    }
}
