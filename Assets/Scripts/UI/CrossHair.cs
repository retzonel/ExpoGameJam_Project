using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    public Transform crossHair;

    void Update()
    {
        var pos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crossHair.position = Vector2.Lerp(crossHair.position, pos, 30f * Time.deltaTime);
    }
}
