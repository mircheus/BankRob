using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class SceneLoader : MonoBehaviour
{
    private const int MainMenuIndex = 0;
    private const int LoadingIndex = 1;
    private const int GameLevelIndex = 2;

    public void LoadGameLevelScene()
    {
        SceneManager.LoadScene(GameLevelIndex);
    }

    public void LoadLoadingScene()
    {
        SceneManager.LoadScene(LoadingIndex);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(MainMenuIndex);
    }
}
