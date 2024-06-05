using UnityEngine;
using Zenject;

public class CameraFollow : MonoBehaviour
{
    private const float OffsetZ = -10;

    private Transform _followingTarget;

    [Inject]
    private void Construct(Player player)
    {
        _followingTarget = player.transform;
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
