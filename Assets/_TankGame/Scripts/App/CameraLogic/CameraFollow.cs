using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private const float OffsetZ = -10;

    [SerializeField] private Transform _followingTarget;

    private void OnEnable()
    {
        LoadLevelState.OnPlayerSpawned += RecievePlayer;
    }

    private void OnDisable()
    {
        LoadLevelState.OnPlayerSpawned -= RecievePlayer;
    }

    private void RecievePlayer(IPlayer player)
    {
        _followingTarget = player.Transform;
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
