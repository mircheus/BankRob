using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using UnityEngine.SceneManagement;

public class YandexInitializer : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    
#if UNITY_EDITOR  
    private void Start()
    {
        _sceneLoader.LoadGameLevelScene();
        Debug.Log("YandexSDK initialization skip");
    }
#endif
    
#if UNITY_WEBGL && !UNITY_EDITOR
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize(OnInitialize);
    }
#endif

    private void OnInitialize()
    {
        _sceneLoader.LoadGameLevelScene();
        Debug.Log("SDK successfully initialized");
    }
}
