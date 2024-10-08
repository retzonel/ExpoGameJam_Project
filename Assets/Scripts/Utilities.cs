using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
       dir = dir.normalized;
       float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
       if (n<0) n += 360;
       return n;
    }

    public static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f)).normalized;
    }
}