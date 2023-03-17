using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class SceneLoader : MonoBehaviour
{   
    
    public void LoadZeroScene()
    {
        SceneManager.LoadScene(0);
    }
}
