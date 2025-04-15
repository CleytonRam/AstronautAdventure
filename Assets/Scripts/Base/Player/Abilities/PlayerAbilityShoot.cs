using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilitiesBase
{
    public List<GunBase> guns;
    public Transform gunPosition;


    private GunBase _currentGun;
    private int _currentGunIndex = 0;

    protected override void Init()
    {
        base.Init();
        CreateGun();
        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
        inputs.Gameplay.ChangeGun.performed += ctx => OnSwitchWeapon();

    }

    private void CreateGun()
    {
        if(_currentGun != null)
        {
            Destroy(_currentGun.gameObject);
        }

        Debug.Log($"Creating gun at index {_currentGunIndex}");
        _currentGun = Instantiate(guns[_currentGunIndex], gunPosition);
        _currentGun.gameObject.SetActive(true);
        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;

    }

    private void StartShoot() 
    {
        Debug.Log("Shoot action triggered");
        _currentGun.StartShoot();
    }
    private void CancelShoot()
    {
        Debug.Log("Shoot action Canceled");
        _currentGun.StopShoot();
    }

    private void OnSwitchWeapon() 
    {
        
        
            Debug.Log("Switch weapon action triggered");
            _currentGunIndex = (_currentGunIndex + 1) % guns.Count;
            CreateGun();
        
    }
 
}
