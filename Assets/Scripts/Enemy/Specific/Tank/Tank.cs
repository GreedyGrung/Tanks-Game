using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy
{
    public TankMoveState MoveState { get; private set; }
    public TankAttackState AttackState { get; private set; }

    [SerializeField] private MoveStateData _moveStateData;
    [SerializeField] private Transform _wallCheck;

    public override void Start()
    {
        base.Start();

        ProjectilePool = FindObjectOfType<ArmorPiercingProjectilePool>();

        MoveState = new(this, StateMachine, _moveStateData);
        AttackState = new(this, StateMachine);

        StateMachine.Initialize(MoveState);
    }

    public void Move()
    {
        transform.Translate(Vector3.up * _moveStateData.MovementSpeed * Time.deltaTime);
    }

    public bool CheckForWallCollision()
    {
        Vector2 raycastOrigin = _wallCheck.position;
        Vector2 raycastDirection = transform.up;

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDirection, EnemyData.WallCheckDistance, EnemyData.ObstacleLayer);

        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    public void RotateRandomly()
    {
        int direction = Random.Range(0, 2);
        float randomRotation = direction == 0 ? 90f : -90f;
        transform.Rotate(Vector3.forward, randomRotation);
    }

    private void OnDrawGizmos()
    {
        Vector2 raycastOrigin = _wallCheck.position;
        Vector2 raycastDirection = transform.up;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(raycastOrigin, raycastOrigin + raycastDirection * EnemyData.WallCheckDistance);
    }
}
