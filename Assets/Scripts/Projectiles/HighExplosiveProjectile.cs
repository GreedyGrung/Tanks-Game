using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighExplosiveProjectile : Projectile
{
    public override void Explode()
    {
        Debug.Log("HighExplosiveProjectile explode");
    }

    public override void Update()
    {
        base.Update();
    }
}
