using UnityEngine;

[RequireComponent(typeof(MonsterNavMesh), typeof(MonsterChasing))]
public class MonsterController : MonoBehaviour
{
    private MonsterNavMesh _monsterNavMesh;
    private MonsterChasing _monsterChasing;

    private void OnEnable()
    {
        _monsterChasing.OnStartChasing += DisablePatrol;
        _monsterChasing.OnStopChasing += EnablePatrol;
    }

    private void Awake()
    {
        _monsterNavMesh = GetComponent<MonsterNavMesh>();
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
