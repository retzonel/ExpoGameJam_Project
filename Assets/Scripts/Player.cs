using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float CurrentHealth { get; private set; }
    [SerializeField] private float startHealth = 10f;

    public PlayerAirTank playerAirTank {get; private set;}
    public PlayerFishCatching playerFishCatching {get; private set;}

    public static Player instance;
    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    void Start()
    {
        playerAirTank = GetComponent<PlayerAirTank>();
        playerFishCatching = GetComponent<PlayerFishCatching>();
        CurrentHealth = startHealth;
    }

    void Update()
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, startHealth);
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void ReduceHealthBy(float amount)
    {
        CurrentHealth -= amount;
    }

    public void Die()
    {
        GameManager.instance.SetGameState(GameState.GameOver);
        Debug.Log("Player Dead");
    }


}
