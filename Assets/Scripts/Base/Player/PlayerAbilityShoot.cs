using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilitiesBase
{

    public GunBase gunBase;

    protected override void Init()
    {
        base.Init();
        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();

    }



    private void StartShoot() 
    {
        Debug.Log("Shoot action triggered");
        gunBase.StartShoot();
    }
    private void CancelShoot()
    {
        Debug.Log("Shoot action Canceled");
        gunBase.StopShoot();
    }
}
