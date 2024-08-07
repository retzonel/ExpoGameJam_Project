using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState GameState {get; private set;}
    public bool MissionComplete { get; private set; }

    public static GameManager instance;


    public event EventHandler OnStateChange;


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

    }

    void Update()
    {

    }

    public void SetMissionState(bool _)
    {
        MissionComplete = _;
    }

    public void SetGameState(GameState _)
    {
        OnStateChange?.Invoke(this, EventArgs.Empty);
        GameState = _;
    }

    public bool IsGameOver()
    {
        return GameState == GameState.GameOver;
    }
}

public enum GameState { Playing, GameOver }
