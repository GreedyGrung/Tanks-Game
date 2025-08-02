using UnityEngine;

namespace TankGame.Runtime.CameraLogic
{
    [RequireComponent(typeof(Camera))]
    public class CameraFollow : MonoBehaviour
    {
        private const float OffsetZ = -10;

        [SerializeField] private Transform _followingTarget;

        public void Initialize(Transform playerTransform)
        {
            _followingTarget = playerTransform;
        }

        private void LateUpdate()
        {
            if (_followingTarget == null)
            {
                return;
            }

            var rotation = Quaternion.Euler(0, 0, 0);
            var position = new Vector3(
                _followingTarget.position.x,
                _followingTarget.position.y,
                _followingTarget.position.z + OffsetZ);

            transform.rotation = rotation;
            transform.position = position;
        }
    }
}