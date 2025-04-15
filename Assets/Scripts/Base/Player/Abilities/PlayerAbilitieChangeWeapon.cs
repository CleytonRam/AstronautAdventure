using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAbilitieChangeWeapon : PlayerAbilitiesBase
{
    public List<GunBase> guns;


    private int _currentGunIndex = 0;

    protected override void Init()
    {
        base.Init();
        SelectWeapon(_currentGunIndex);

    }

    private void OnSwitchWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("GUN SWITCHED");
            _currentGunIndex = (_currentGunIndex + 1) % guns.Count;
           SelectWeapon(_currentGunIndex);

        }
    }

    private void SelectWeapon(int index) 
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].gameObject.SetActive(i == index);
        }
    }

    protected override void RegisterListeners()
    {
        base.RegisterListeners();
        inputs.Gameplay.ChangeGun.performed += OnSwitchWeapon;
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        inputs.Gameplay.ChangeGun.performed -= OnSwitchWeapon;
    }
}
