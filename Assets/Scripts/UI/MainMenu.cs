using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // 加载第一关的九宫格
        SceneManager.LoadSceneAsync("Level1");
    }

    
    public void QuitGame()
    {
        
        Application.Quit();

    }
}
