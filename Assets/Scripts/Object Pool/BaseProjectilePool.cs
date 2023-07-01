using UnityEngine;

public abstract class BaseProjectilePool : MonoBehaviour
{
    public ObjectPool<Projectile> Pool { get; protected set; }
}
