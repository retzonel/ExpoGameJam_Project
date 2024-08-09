using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirTankUi : MonoBehaviour
{
    [SerializeField] Image fill;
    void Update()
    {
        fill.fillAmount = Mathf.Lerp(fill.fillAmount, Player.instance.PlayerAirTank.currentAir/Player.instance.PlayerAirTank.maxAir, 3f * Time.deltaTime);
    }
}
