using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public Transform positionToshoot;
    public float timeBetweenShoot = .3f;
    public float speed = 50f;

    private Coroutine _currentCourotine;


    protected virtual IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }


    public virtual void Shoot()
    {
        if(prefabProjectile == null)
        {
            Debug.LogError("Prefab projectile is not assigned");
            return;
        }
        if(positionToshoot == null)
        {
            Debug.LogError("Position to shoot is not assigned");
            return;
        }


        var projectile = Instantiate(prefabProjectile, positionToshoot.position, positionToshoot.rotation);
        //projectile.transform.position = positionToshoot.position;
        //projectile.transform.rotation = positionToshoot.rotation;
        
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = positionToshoot.forward * speed;
        }

        else
        {
            Debug.LogError("Projectile does not have a Rigidbody component");
        }

        Destroy(projectile.gameObject, projectile.timeToDestroy);

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
