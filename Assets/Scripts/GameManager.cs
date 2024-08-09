using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState GameState { get; private set; }
    public GameEndType GameEndType { get; private set; }
    public bool MissionComplete { get; private set; }

    public static GameManager instance;


    public event EventHandler OnStateChange;
    public event EventHandler OnGameEnd;

    public event EventHandler OnGamePause;
    private bool isGamePaused = false;

    public int currentLevel = 1;
    public int LevelReached
    {
        get
        {
            return PlayerPrefs.GetInt("levelReached", 1);
        }
        set
        {
            PlayerPrefs.SetInt("levelReached", value);
        }
    }


    private void Awake()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    void Start()
    {
        GameEndType = GameEndType.NotDead;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
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

    public void SetGameEndType(GameEndType _)
    {
        GameEndType = _;
    }

    public bool IsGameOver()
    {
        return GameState == GameState.GameOver;
    }

    public void GameEnd()
    {
        if (GameEndType == GameEndType.MissionCompleteBoat)
        {
            LevelReached = (currentLevel + 1);
        }
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {

            Time.timeScale = 0;
            OnGamePause?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1;
            OnGamePause?.Invoke(this, EventArgs.Empty);
        }
    }
}

public enum GameState { Playing, GameOver }
public enum GameEndType
{
    NotDead,
    MissionCompleteBoat,
    MissionCompleteDeath,
    MissionNotCompleteDeath,
}
