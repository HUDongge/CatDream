using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : SingletonMono<ScenesManager>
{
    

    // 存储每个场景的入口状态
    private Dictionary<string, SceneEntrance> sceneEntrances;

    public string Direction; //来的方向

    public int currentScore;  //本关卡当前得分

    public int currentLevelIndex;    //看是第几关
    public string playerInScene;    //当前玩家所在第几关

    public LevelOneEntrance levelOne;
    public LevelTwoEntrance levelTwo;
    public LevelThreeEntrance levelThree;

    public GridSelector gridSelector;

    private bool ToggleScene = false;

    //  public List<Vector2> startPoint; 存储不同场景进入时动态生成玩家的位置

    private void Awake()
    {
        base.Awake();
        currentLevelIndex = 1;  //从第一关开始
        levelOne = new LevelOneEntrance();
        levelTwo = new LevelTwoEntrance();
        levelThree = new LevelThreeEntrance();

        sceneEntrances = levelOne.sceneEntrances;
        playerInScene = "Level1_0";

        SceneManager.sceneLoaded += OnSceneLoaded;
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

    // 切换场景的逻辑SwitchScene(玩家所在场景九宫格位置，来的方向)
    public void SwitchScene(int currentPos, string direction )
    {     
        if(direction=="right")
        {
            Debug.Log($"direction:{direction}");

            Direction = "right";

            string nextScene = GetData.Instance.GetNameByIndex(currentPos + 1);

            if (((currentPos + 1)% 3 == 0)|| nextScene==null)  //在九宫格最右边了，不能再往右走
            {
                return;
            }
            else if (sceneEntrances[nextScene].canPassLeft)  //否则向右走了一格，找到该格子对应的场景
            {
                //  GridSelector.Instance.GetNameByIndex();
                SceneManager.LoadSceneAsync(nextScene);           
            }    
        }
        else if(direction=="left")
        {
            Direction = "left";

            string nextScene = GetData.Instance.GetNameByIndex(currentPos - 1);

            if ((currentPos % 3 == 0)|| nextScene == null)  //当前场景在九宫格的最左边，不能继续往左走
            {
                return;
            }
            else if(sceneEntrances[nextScene].canPassRight)
            {
                SceneManager.LoadSceneAsync(nextScene);
            }
        }
        else if(direction=="down")
        {
            Direction = "down";

            string nextScene = GetData.Instance.GetNameByIndex(currentPos + 3);

            if (nextScene==null||currentPos == 6|| currentPos == 7|| currentPos == 8)
            {
                return;
            }
            else if(sceneEntrances[nextScene].canPassUp)
            {
                SceneManager.LoadSceneAsync(nextScene);
            }
        }
       


    }

  
    
    public void ResetAll()
    {
        currentScore = 0;
        Direction = "";
        playerInScene = "Level" + currentLevelIndex + "_" + 1;

        //玩家重生在出发点：

        //九宫格恢复原始：
    }

    void InitialEntrances(int nextIndex)
    {      
        if (nextIndex == 2)
        {
            sceneEntrances = levelTwo.sceneEntrances;
            playerInScene = "Level2_0";
        }
        else if(nextIndex == 3)
        {
            sceneEntrances = levelThree.sceneEntrances;
            playerInScene = "Level3_0";
        }
        else
        {
            sceneEntrances = levelOne.sceneEntrances;  //否则回到主界面，重新设置为第一关的入口信息
            playerInScene = "Level1_0";
        }

    }


    private void Update()
    {
        if(currentScore==2)
        {
            ResetAll();
            InitialEntrances(currentLevelIndex + 1);
            SceneManager.LoadSceneAsync(currentLevelIndex +1); 
           // Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            //if (gridSelector != null)
            //gridSelector.SaveToSingleton();

            string currentActiveScene = SceneManager.GetActiveScene().name;

            if (ToggleScene)  //有下划线说明在小场景，没有说明在九宫格
            {
                SceneManager.UnloadSceneAsync("Level1_0");
                ToggleScene = !ToggleScene;
                gridSelector.isSmallLevelOn = false;
            }  //按F切换到九宫格场景
            else
            {
                SceneManager.LoadSceneAsync("Level1_0", LoadSceneMode.Additive);
                ToggleScene = !ToggleScene;
                gridSelector.isSmallLevelOn = true;
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gridSelector = FindObjectOfType<GridSelector>();

        if (gridSelector != null)
        {
            Debug.Log($"GridSelector initialized in scene: {scene.name}");
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


}