using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ProjectileBase : MonoBehaviour
{

    public float speed = 50f;
    public float timeToDestroy = 2f;
    public int dameAmount = 1;

    public List<string> tagsToHit;


    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (var tag in tagsToHit)
        {

            if (collision.transform.tag == tag)
            {
                var damageable = collision.gameObject.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    Vector3 dir = collision.transform.position - transform.position;
                    dir = -dir.normalized;
                    dir.y = 0;
                    damageable.Damage(dameAmount, dir);
                }
                Destroy(gameObject);
                return;
            }
        }
     
        var projectille = collision.gameObject.GetComponent<ProjectileBase>();
        if (projectille == null)
        {
            Destroy(projectille.gameObject);
        }
    }
}
