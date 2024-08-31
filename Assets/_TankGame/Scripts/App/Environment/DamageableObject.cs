using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable
{
    public void TakeDamage(float damage)
    {
        Debug.Log("Take damage: " + damage);
    }
}
