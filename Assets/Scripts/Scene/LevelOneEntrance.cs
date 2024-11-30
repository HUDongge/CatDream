using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneEntrance 
{
    // 初始化场景入口状态：上下左右
        public Dictionary<string,SceneEntrance> sceneEntrances = new Dictionary<string, SceneEntrance>
        {
            { "Level1_0", new SceneEntrance(false, false, false, true) }, 
            { "Level1_1", new SceneEntrance(false, false, true, true) },
            { "Level1_2", new SceneEntrance(false, true, true, false) },
            { "Level1_3", new SceneEntrance(false, true, false, true) },
            { "Level1_4", new SceneEntrance(false, false, true, true) },
            { "Level1_5", new SceneEntrance(true, false, true, false) },
            { "Level1_6", new SceneEntrance(true, false, false, true) },
            { "Level1_7", new SceneEntrance(false, false, true, false) },
            { "Empty",    new SceneEntrance(false, false, false, false) }
        };
}
