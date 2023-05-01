using System;
using UnityEngine;

[RequireComponent(typeof(MonsterPatrol), typeof(MonsterChasing))]
public sealed class MonsterController : MonoBehaviour
{
    private MonsterPatrol _monsterNavMesh;
    private MonsterChasing _monsterChasing;

    private Animator _animator;

    private void OnEnable()
    {
        _monsterChasing.OnStartChasing += DisablePatrol;
        _monsterChasing.OnStopChasing += EnablePatrol;
        _animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        _monsterNavMesh = GetComponent<MonsterPatrol>();
        _monsterChasing = GetComponent<MonsterChasing>();
    }

    private void Update()
    {
        if (_monsterChasing._isChasing & !_monsterNavMesh._isWaiting)
        {
            _animator.SetBool("IsRunning",true);
            _animator.SetBool("IsWalking", false);
        }
        else if (_monsterNavMesh._isWaiting & !_monsterChasing._isChasing)
        {
            _animator.SetBool("IsRunning",false);
            _animator.SetBool("IsWalking",false);
        }
        else if (!_monsterNavMesh._isWaiting & !_monsterChasing._isChasing)
        {
            _animator.SetBool("IsRunning",false);
            _animator.SetBool("IsWalking",true);
        }
    }

    private void DisablePatrol()
    {
        _monsterNavMesh.enabled = false;
    }

    private void EnablePatrol()
    {
        _monsterNavMesh.enabled = true;
    }
}
