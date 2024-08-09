using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAirTank : MonoBehaviour
{
    public bool IsInWater {get; private set;} = true;
    public float maxAir = 100;
    public float currentAir;
    [SerializeField] private float airDelpletionRate = 5f;
    [SerializeField] private float healthReductionRate = 1f;

    Player player;
    [Space]
    [SerializeField] ParticleSystem buubleEffect;
    bool isBubblePlaying = false;

    void Start()
    {
        currentAir = maxAir;
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (IsInWater)
        {
            if (currentAir > 0)
            {
                currentAir -= airDelpletionRate * Time.deltaTime;
            } else {
                player.ReduceHealthBy(healthReductionRate);
            }
            if(!isBubblePlaying){
                buubleEffect.Play();
                isBubblePlaying = true;
            }
        } else {
            buubleEffect.Stop();
            isBubblePlaying = false;
        }

        currentAir = Mathf.Clamp(currentAir, 0, maxAir);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Surface"))
        {
            IsInWater = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Surface"))
        {
            IsInWater = true;
        }
    }

    public void ResetAirTank()
    {
        currentAir = maxAir;
    }
}
