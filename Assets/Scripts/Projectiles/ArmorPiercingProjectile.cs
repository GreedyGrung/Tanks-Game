using UnityEngine;

public class ArmorPiercingProjectile : Projectile
{
    public override void Explode()
    {
        Debug.Log("ArmorPiercingProjectile explode");
    }
}
