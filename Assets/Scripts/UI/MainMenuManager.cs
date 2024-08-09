using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button quitBtn;
    public Button[] playBtns;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        quitBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        for (int i = 0; i < playBtns.Length; i++)
        {
            if ((i + 1) > GameManager.instance.LevelReached)
            {
                playBtns[i].interactable = false;
            }
        }

    }

    public void PlayLevel(int level)
    {
        LevelLoader.LoadLevel(level);
    }

    public void ResetLevels()
    {
        PlayerPrefs.DeleteAll();
        LevelLoader.LoadLevel(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
