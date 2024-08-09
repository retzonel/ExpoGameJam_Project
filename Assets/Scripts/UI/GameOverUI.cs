using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button menuBtn;
    [SerializeField] private Button playLvlAgain;


    [SerializeField] TextMeshProUGUI gameEndTypeText;

    [SerializeField] string missionCompleteBoatRemark;
    [SerializeField] string missionCompleteDeathRemark;
    [SerializeField] string missionNotCompleteDeathRemark;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.OnStateChange += GameManager_OnGameStateChanged;
        Hide();
        menuBtn.onClick.AddListener(() =>
        {
            LevelLoader.LoadLevel(0);
        });
        playLvlAgain.onClick.AddListener(() =>
        {
            LevelLoader.LoadLevel(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        });
    }

    void GameManager_OnGameStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.instance.IsGameOver())
        {
            UpdateUI();
            Show();
        }
        else Hide();
    }

    private void Show()
    {
        
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        
        gameObject.SetActive(false);

    }

    void UpdateUI()
    {
        switch (GameManager.instance.GameEndType)
        {
            case GameEndType.MissionCompleteBoat:
                gameEndTypeText.text = missionCompleteBoatRemark;
                break;
            case GameEndType.MissionCompleteDeath:
                gameEndTypeText.text = missionCompleteDeathRemark;
                break;
            case GameEndType.MissionNotCompleteDeath:
                gameEndTypeText.text = missionNotCompleteDeathRemark;
                break;
        }
    }
}
