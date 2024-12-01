using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    public ScenesManager scenesManager;

    void Update()
    {
        if (scenesManager == null)
        {
            scenesManager = FindObjectOfType<ScenesManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int currentLevel= ScenesManager.Instance.currentLevelIndex;
        if (other.CompareTag("Player")&& ScenesManager.Instance.currentScore==2)
        {
            //SceneManager.LoadSceneAsync("MainMenu");
            if (currentLevel == 1)
            {
                SceneManager.LoadSceneAsync("Level2");
                ScenesManager.Instance.ResetAll();
                scenesManager.currentActiveScene = "NULL";
                scenesManager.ToggleScene = false;
            }
            else
            {
                SceneManager.LoadSceneAsync("MainMenu");
            }
            Destroy(this.gameObject);
        }

    }
}
