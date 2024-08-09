using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    public Button resumeBtn;
    public Button menuBtn;
        [SerializeField] RectTransform pauseBtn;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.OnGamePause += GameManager_OnGamePaused;
        Hide();
        resumeBtn.onClick.AddListener(() =>
        {
            GameManager.instance.TogglePause();
        });
        menuBtn.onClick.AddListener(() =>
        {
            LevelLoader.LoadLevel(0);
        });
    }

    void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        if (GameManager.instance.IsGamePaused())
        {
            Show();
        }
        else Hide();
    }

    private void Show()
    {
        pauseBtn.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        pauseBtn.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
