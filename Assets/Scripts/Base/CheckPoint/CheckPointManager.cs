using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class CheckPointManager : Singleton<CheckPointManager>
{
    public int lastCheckPoint = 0;
    public List<CheckPointBase> checkPoints;

    public bool HasCheckPoint()
    {
        return lastCheckPoint > 0;
    }

    public void SaveCheckPoint(int i)
    {
        if (lastCheckPoint < i)
        {
            lastCheckPoint = i;
        }
    }


    public Vector3 GetPositionFromLastCheckPoint()
    {
        var checkpoint = checkPoints.Find(i => i.key == lastCheckPoint);
        return checkpoint.transform.position;
    }
}
