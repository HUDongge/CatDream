using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoEntrance : MonoBehaviour
{
    // 初始化场景入口状态：上下左右
    public Dictionary<string, SceneEntrance> sceneEntrances = new Dictionary<string, SceneEntrance>
        {
            { "Level2_0", new SceneEntrance(false, false, false, true) },
            { "Level2_1", new SceneEntrance(true, false, true, true) },
            { "Level2_2", new SceneEntrance(false, false, true, true) },
            { "Level2_3", new SceneEntrance(true, false, true, false) },
            { "Level2_4", new SceneEntrance(false, true, true, true) },
            { "Level2_5", new SceneEntrance(false, true, false, true) },
            { "Level2_6", new SceneEntrance(false, false, false, true) },
            { "Level2_7", new SceneEntrance(false, false, false, false) },
            { "Empty",    new SceneEntrance(false, false, false, true) }
        };
}