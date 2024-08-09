using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float CurrentHealth { get; private set; }
    public float startHealth = 10f;

    public PlayerAirTank PlayerAirTank { get; private set; }
    public PlayerGetObjective PlayerGetObjective { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }

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
        PlayerAirTank = GetComponent<PlayerAirTank>();
        PlayerGetObjective = GetComponent<PlayerGetObjective>();
        PlayerMovement = GetComponent<PlayerMovement>();
        CurrentHealth = startHealth;
    }

    void Update()
    {
        if (GameManager.instance.IsGameOver() == true || GameManager.instance.IsGamePaused() == true)
        {
            return;
        }
        
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
        if (GameManager.instance.MissionComplete == true)
        {
            GameManager.instance.SetGameEndType(GameEndType.MissionCompleteDeath);
        }
        else if (GameManager.instance.MissionComplete == false)
        {
            GameManager.instance.SetGameEndType(GameEndType.MissionNotCompleteDeath);
        }
        Debug.Log("Player Dead");
    }

}
