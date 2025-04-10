using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ProjectileBase : MonoBehaviour
{

    public float speed = 50f;
    public float timeToDestroy = 2f;
    public int dameAmount = 1;


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
    }
}
