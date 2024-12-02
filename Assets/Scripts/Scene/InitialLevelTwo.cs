using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialLevelTwo : MonoBehaviour
{
    
    void Start()
    {
        ScenesManager.Instance.sceneEntrances=new LevelTwoEntrance().sceneEntrances;
        ScenesManager.Instance.playerInScene = "Leve2_0";
        ScenesManager.Instance.currentScore = 0;
        ScenesManager.Instance.isFirstCome = false;
        
    }

  
}
