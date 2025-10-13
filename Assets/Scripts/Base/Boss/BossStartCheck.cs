using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartCheck : MonoBehaviour
{
    public string tagToCheck = "Player";
    public GameObject bossCamera;
    


    private void Awake()
    {
        bossCamera.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == tag) 
        {
            TurnCameraOn();
        }
    }


    private void TurnCameraOn() 
    {
        bossCamera.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, transform.localScale.y);
    }
}
