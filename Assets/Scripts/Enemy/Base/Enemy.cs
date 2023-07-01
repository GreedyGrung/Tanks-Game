using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyData _enemyData;

    public StateMachine StateMachine { get; private set; }
    public Transform Player { get; private set; }
    public EnemyData EnemyData => _enemyData;

    public virtual void Start()
    {
        StateMachine = new();
        Player = FindObjectOfType<PlayerMovement>().transform;
    }

    public virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual void TakeDamage(float damage)
    {
        
    }
}
