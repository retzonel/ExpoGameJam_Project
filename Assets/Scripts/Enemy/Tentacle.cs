using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public int length;
    public LineRenderer lineRend;
    public Vector3[] segmentPos;
    private Vector3[] segmentV;

    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;

    // Start is called before the first frame update
    void Start()
    {
        lineRend.positionCount = length;
        segmentPos = new Vector3[length];
        segmentV = new Vector3[length];
        
    }

    // Update is called once per frame
    void Update()
    {
        segmentPos[0] = targetDir.position;
        for (int i = 1; i < segmentPos.Length; i++)
        {
            segmentPos[i] = Vector3.SmoothDamp(segmentPos[i], segmentPos[i - 1], ref segmentV[i],smoothSpeed);
        }
        lineRend.SetPositions(segmentPos);
    }
}

