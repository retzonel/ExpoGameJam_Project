using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAirTank : MonoBehaviour
{
    bool isInWater = true;
    [SerializeField] private float maxAir = 100;
    [SerializeField] private float currentAir;
    [SerializeField] private float airDelpletionRate = 5f;
    [SerializeField] private float healthReductionRate = 1f;

    Player player;

    void Start()
    {
        currentAir = maxAir;
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (isInWater)
        {
            if (currentAir > 0)
            {
                currentAir -= airDelpletionRate * Time.deltaTime;
            } else {
                player.ReduceHealthBy(healthReductionRate);
            }
        }

        currentAir = Mathf.Clamp(currentAir, 0, maxAir);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Surface"))
        {
            isInWater = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Surface"))
        {
            isInWater = true;
        }
    }

}
