using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThreeEntrance : MonoBehaviour
{
    //场景还未搭建好，复制的关卡二的数据
    public Dictionary<string, SceneEntrance> sceneEntrances = new Dictionary<string, SceneEntrance>
        {
            { "Level3_1", new SceneEntrance(false, false, false, true) },
            { "Level3_2", new SceneEntrance(true, false, true, true) },
            { "Level3_3", new SceneEntrance(false, false, true, true) },
            { "Level3_4", new SceneEntrance(true, false, true, false) },
            { "Level3_5", new SceneEntrance(false, true, true, true) },
            { "Level3_6", new SceneEntrance(false, true, false, true) },
            { "Level3_7", new SceneEntrance(false, false, false, true) },
            { "Level3_8", new SceneEntrance(false, false, false, false) },
            { "Empty",    new SceneEntrance(false, false, false, true) }
        };
}
