using UnityEngine;

public class HighExplosiveProjectile : Projectile
{
    public override void Explode()
    {
        Debug.Log("HighExplosiveProjectile explode");
    }
}
