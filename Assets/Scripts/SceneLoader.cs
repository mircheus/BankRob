using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class SceneLoader : MonoBehaviour
{
    private const int LoadingIndex = 0;
    private const int MainMenuIndex = 1;
    private const int GameLevelIndex = 2;
    private const int TutorialLevel1Index = 3;
    private const int TutorialLevel2Index = 4;

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

    public void LoadTutorialLevel1()
    {
        SceneManager.LoadScene(TutorialLevel1Index);
    }

    public void LoadTutorialLevel2()
    {
        SceneManager.LoadScene(TutorialLevel2Index);
    }
}
