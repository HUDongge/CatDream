using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    public int currentLevel = 1;      // 当前关卡
    public int totalLevels = 3;       // 总关卡数
    public int currentScore = 0;      // 当前分数
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
        }
        else
        {
            Debug.Log("Game Over! You have completed all the levels.");
            SceneManager.LoadSceneAsync("MainMenu");  //游戏结束返回主界面
        }
    }

    // 更新分数
    public void UpdateScore(int score)
    {
        currentScore += score;

        // 如果达成当前关卡目标分数，加载下一个关卡
        if (currentScore >= scorePerLevel)
        {
            currentLevel++;
            currentScore = 0;
            LoadLevel(currentLevel);
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
