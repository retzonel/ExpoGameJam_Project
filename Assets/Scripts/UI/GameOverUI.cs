using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button menuBtn;
    [SerializeField] private Button playLvlAgain;
    
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
            
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameManager_OnGameStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.instance.IsGameOver())
        {
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
}
