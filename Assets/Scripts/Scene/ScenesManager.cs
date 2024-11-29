using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    // 单例实例
    private static ScenesManager instance;

    // 获取单例实例
    public static ScenesManager Instance
    {
        get
        {
            if (instance == null)
            {
                // 尝试查找现有的实例
                instance = FindObjectOfType<ScenesManager>();

                // 如果还没有实例，创建一个新的实例
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(ScenesManager).Name);
                    instance = singletonObject.AddComponent<ScenesManager>();
                    DontDestroyOnLoad(singletonObject);  // 确保在场景切换时不会销毁
                }
            }
            return instance;
        }
    }

    // 存储每个场景的入口状态
    private Dictionary<string, SceneEntrance> sceneEntrances;

    public string Direction; //来的方向

    private void Awake()
    {
        // 确保只有一个实例存在
        if (instance != null && instance != this)
        {
            Destroy(gameObject);  // 如果已经有实例了，就销毁当前对象
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 保持场景管理器在切换场景时不被销毁
        }

        // 初始化场景入口状态：上下左右
        sceneEntrances = new Dictionary<string, SceneEntrance>
        {
            { "Level1_1", new SceneEntrance(false, false, false, true) }, 
            { "Level1_2", new SceneEntrance(false, false, true, true) },
            { "Level1_3", new SceneEntrance(false, true, true, true) },
            { "Level1_4", new SceneEntrance(false, true, false, false) },
            { "Level1_5", new SceneEntrance(false, false, true, false) },
            { "Level1_6", new SceneEntrance(false, false, false, true) },
            { "Level1_7", new SceneEntrance(false, true, false, false) },
            { "Level1_8", new SceneEntrance(false, false, false, false) }
        };
    }

    // 获取当前场景的入口状态
    public SceneEntrance GetSceneEntrance(string sceneName)
    {
        if (sceneEntrances.ContainsKey(sceneName))
        {
            return sceneEntrances[sceneName];
        }
        else
        {
            Debug.LogError("Scene not found: " + sceneName);
            return null;
        }
    }

    // 切换场景的逻辑
    public void SwitchScene(int currentSceneIndex,string direction )
    {
        int currentPos = SlideTest.sceneToPos[currentSceneIndex];//查找当前玩家所在场景的在九宫格里的位置
        if(direction=="right")
        {
            Debug.Log($"direction:{direction}");

            Direction = "right";
         
            if ((currentPos + 1)% 3 == 0)  //在九宫格最右边了，不能再往右走
            {
                return;
            }
            else if (sceneEntrances[SlideTest.PosToScene[currentPos+1]].canPassLeft)  //否则向右走了一格，找到该格子对应的场景
            {
                SceneManager.LoadScene(SlideTest.PosToScene[currentPos + 1]);

            }    
        }
       


    }
   
}