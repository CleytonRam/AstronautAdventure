using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public Transform positionToshoot;
    public float timeBetweenShoot = .3f;


    private Coroutine _currentCourotine;


    protected virtual IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }


    public void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToshoot.position;
        projectile.transform.rotation = positionToshoot.rotation;

    }

    public void StartShoot()
    {
        StopShoot();
        _currentCourotine = StartCoroutine(ShootCoroutine());

    }

    public void StopShoot()
    {
        if (_currentCourotine != null)
        {
            StopCoroutine(_currentCourotine);
        }
    }
}
