using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public Transform positionToshoot;
    public float timeBetweenShoot = .3f;


    private Coroutine _currentCourotine;
    public KeyCode keyShoot = KeyCode.S;


   

    void Update()
    {
        if (Input.GetKeyDown(keyShoot))
        {
            _currentCourotine = StartCoroutine(StartShoot());
        }
        else if (Input.GetKeyUp(keyShoot))
        {
            if (_currentCourotine != null)
            {
                StopCoroutine(_currentCourotine);
            }
        }
    }

    IEnumerator StartShoot()
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
}
