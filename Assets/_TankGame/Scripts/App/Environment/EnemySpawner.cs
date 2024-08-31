using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyTypeId _enemyType;

    public EnemyTypeId EnemyType => _enemyType;
}
