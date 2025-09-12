using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 01;
    
    private string checkpointKey = "CheckPointKey";
    private bool checkpointActivated = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!checkpointActivated && other.transform.tag == "Player")
        {
            CheckCheckPoint();
        }
    }

    private void CheckCheckPoint() 
    {
        TurnItOn();
        SaveCheckPoint();
    }

    [NaughtyAttributes.Button]
    private void TurnItOn() 
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }
    [NaughtyAttributes.Button]
    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.grey);
    }
    private void SaveCheckPoint() 
    {
        //if(PlayerPrefs.GetInt(checkpointKey, 0) > key)
        //{
        //    PlayerPrefs.SetInt(checkpointKey, key);         
        //}

        CheckPointManager.Instance.SaveCheckPoint(key);

        checkpointActivated = true;
    }
}
