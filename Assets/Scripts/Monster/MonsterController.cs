using UnityEngine;

[RequireComponent(typeof(MonsterPatrol), typeof(MonsterChasing))]
public sealed class MonsterController : MonoBehaviour
{
    private MonsterPatrol _monsterNavMesh;
    private MonsterChasing _monsterChasing;

    private void OnEnable()
    {
        _monsterChasing.OnStartChasing += DisablePatrol;
        _monsterChasing.OnStopChasing += EnablePatrol;
    }

    private void Awake()
    {
        _monsterNavMesh = GetComponent<MonsterPatrol>();
        _monsterChasing = GetComponent<MonsterChasing>();
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
