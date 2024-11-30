using UnityEngine;
using UnityEngine.SceneManagement;

/*本来想写一个level管理器，当分数够的时候加载到下一关，但是发现不需要，
直接在每一关的场景切换单例管理里判断即可，所以这个暂时作废*/

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    public int currentLevel = 1;      // 当前关卡
    public int totalLevels = 3;       // 总关卡数
    public int scorePerLevel = 3;   // 每关卡目标分数

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);  // 确保只有一个 LevelManager
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 保持 LevelManager 不被销毁
        }
    }

    // 加载指定关卡
    public void LoadLevel(int level)
    {
        if (level <= totalLevels)
        {
            currentLevel = level;
            string sceneName = "Level" + level;  // 加载到下一关的九宫格场景Level1，Level2，Level3
            SceneManager.LoadSceneAsync(sceneName);

            // 为当前关卡创建新的 ScenesManager（管理一个关卡内不同场景切换）实例
            GameObject sceneSwitchManagerObj = new GameObject("ScenesManager");
            sceneSwitchManagerObj.AddComponent<ScenesManager>();

            //加载显示关卡的的分数
        }
        else
        {
            Debug.Log("Game Over! You have completed all the levels.");
            SceneManager.LoadSceneAsync("MainMenu");  //游戏结束返回主界面
        }
    }

    // 从存档里读取每一关的分数，在加载到该关卡时显示
    public void CheckScore(int level)
    {
        // 读取存储的得分
        int currentScore = PlayerPrefs.GetInt("Level"+ level, 0); 

        // 如果达成当前关卡目标分数，加载下一个关卡
        if (currentScore >= scorePerLevel)
        {
            SceneManager.LoadSceneAsync(currentLevel);  //不太对
        }
    }
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
}
