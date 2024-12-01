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

    public string currentActiveScene = "NULL";

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
    public void SwitchScene(int currentPos, string direction)
    {
        Debug.Log($"currentPlayerInPos: {currentPos}");

        // Determine the direction and the next scene
        string nextScene = null;

        if (direction == "right")
        {
            Debug.Log($"direction: {direction}");
            Direction = "right";
            nextScene = GridSelector.GetNameByIndex(currentPos + 1);

            // Check if the next position is valid
            if (((currentPos + 1) % 3 == 0) || nextScene == null)  // Cannot move right if at the rightmost column or invalid
                return;

            if (!sceneEntrances[nextScene].canPassLeft)  // Check for valid scene transition
                return;
        }
        else if (direction == "left")
        {
            Debug.Log($"direction: {direction}");
            Direction = "left";
            nextScene = GridSelector.GetNameByIndex(currentPos - 1);

            // Check if the next position is valid
            if ((currentPos % 3 == 0) || nextScene == null)  // Cannot move left if at the leftmost column or invalid
                return;

            if (!sceneEntrances[nextScene].canPassRight)  // Check for valid scene transition
                return;
        }
        else if (direction == "down")
        {
            Debug.Log($"direction: {direction}");
            Direction = "down";
            nextScene = GridSelector.GetNameByIndex(currentPos + 3);

            // Check if the next position is valid
            if (nextScene == null || currentPos == 6 || currentPos == 7 || currentPos == 8)  // Cannot move down if at the bottom row
                return;

            if (!sceneEntrances[nextScene].canPassUp)  // Check for valid scene transition
                return;
        }

        // If nextScene is valid, proceed with unloading and loading
        if (!string.IsNullOrEmpty(nextScene))
        {
            // Unload the current scene (add logic to keep the main level persistent)
            string currentScene = GridSelector.GetNameByIndex(currentPos);
            if (!string.IsNullOrEmpty(currentScene) && currentScene != "Level1")  // Ensure we don't unload the main level
            {
                SceneManager.UnloadSceneAsync(currentScene);
            }

            // Load the new scene additively
            SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
            Debug.Log($"Switching to scene: {nextScene}");
            currentActiveScene = nextScene;
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

            if (currentActiveScene == "NULL")
            currentActiveScene = gridSelector.GetSelectedObjectName();

            if (ToggleScene)  //有下划线说明在小场景，没有说明在九宫格
            {
                if (!string.IsNullOrEmpty(currentActiveScene) && currentActiveScene != "Level1")
                {
                    Scene sceneToUnload = SceneManager.GetSceneByName(currentActiveScene);

                    if (sceneToUnload.isLoaded)
                    {
                        SceneManager.UnloadSceneAsync(currentActiveScene);
                        Debug.Log($"Unloading scene: {currentActiveScene}");
                    }
                    else
                    {
                        Debug.LogError($"Scene '{currentActiveScene}' is not currently loaded. Cannot unload.");
                    }
                }
                else
                {
                    Debug.LogError("Invalid scene name or trying to unload the main scene.");
                }

                ToggleScene = !ToggleScene;
                gridSelector.isSmallLevelOn = false;
                currentActiveScene = "NULL";
            }  //按F切换到九宫格场景
            else
            {   
                SceneManager.LoadSceneAsync(currentActiveScene, LoadSceneMode.Additive);
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