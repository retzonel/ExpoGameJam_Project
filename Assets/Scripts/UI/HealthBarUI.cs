using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] Image fill;
    void Update()
    {
        fill.fillAmount = Mathf.Lerp(fill.fillAmount, Player.instance.CurrentHealth/Player.instance.startHealth, 3f * Time.deltaTime);
    }
}
