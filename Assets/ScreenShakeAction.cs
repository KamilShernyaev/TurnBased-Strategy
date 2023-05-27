using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeAction : MonoBehaviour
{
    private void Start() {
        ShootAction.OnAnyShoot += ShootAction_OnAnyShoot;
        GrenadeProjectile.OnAnyGrenadeExplosion += GrenadeProjectile_OnAnyGrenadeExplosion;
    }

    private void GrenadeProjectile_OnAnyGrenadeExplosion(object sender, EventArgs e)
    {
        ScreenShake.Instance.Shake(5f);
    }

    private void ShootAction_OnAnyShoot(object sender, ShootAction.OnShootEventArgs e)
    {
        ScreenShake.Instance.Shake();
    }
}
