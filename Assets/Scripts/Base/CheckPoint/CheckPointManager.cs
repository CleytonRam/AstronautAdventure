using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class CheckPointManager : Singleton<CheckPointManager>
{
    public int lastCheckPoint = 0;
    public List<CheckPointBase> checkPoints;

    [SerializeField] private Vector3 _fallbackPosition = new Vector3(0, 1, 0);



    public bool HasCheckPoint()
    {
        return lastCheckPoint > 0;
    }

    public void SaveCheckPoint(int i)
    {
        if (lastCheckPoint < i)
        {
            lastCheckPoint = i;
            Debug.Log("Checkpoint saved: " + lastCheckPoint);
        }
    }


    public Vector3 GetPositionFromLastCheckPoint()
    {
        var checkpoint = checkPoints.Find(i => i.key == lastCheckPoint);

        // Verifica se o checkpoint existe antes de acessá-lo
        if (checkpoint != null)
        {
            return checkpoint.transform.position;
        }
        else
        {
            Debug.LogError($"Checkpoint com key {lastCheckPoint} não encontrado!");
            return _fallbackPosition;
        }
    }
}
