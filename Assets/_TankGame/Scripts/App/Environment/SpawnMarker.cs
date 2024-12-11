using UnityEngine;

public class SpawnMarker : MonoBehaviour
{
    [SerializeField] private EnemyTypeId _enemyType;

    public EnemyTypeId EnemyType => _enemyType;
}
