using TankGame.App.Utils.Enums;
using UnityEngine;

namespace TankGame.App.Environment
{
    public class SpawnMarker : MonoBehaviour
    {
        [SerializeField] private EnemyTypeId _enemyType;
        [SerializeField] private bool _isRandom;

        public EnemyTypeId EnemyType => _enemyType;
        public bool IsRandom => _isRandom;
    }
}