﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManage : MonoBehaviour
{ 
    public Button returnSlideButton;  //#
    public Button resetButton;  //重置
    public Button returnInfoButton;  //暂时当退出按钮

    void Start()
    {

        returnSlideButton.onClick.AddListener(OnSlideClick);  // 添加点击事件监听
        resetButton.onClick.AddListener(OnResetClick);
        returnInfoButton.onClick.AddListener(OnInfoClick);
    }

    void OnSlideClick()
    {
        SceneManager.LoadSceneAsync("Test");  //返回九宫格Level1
    }
    
    void OnResetClick()
    {
        ScenesManager.Instance.currentScore = 0;
        Debug.Log(ScenesManager.Instance.currentScore);
    }

    void OnInfoClick()
    {
        Application.Quit();
    }
   
    
}