using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        int currentLevel= ScenesManager.Instance.currentLevelIndex;
        if (other.CompareTag("Player")&& ScenesManager.Instance.currentScore==2)
        {
            if (currentLevel == 1)
            {
                SceneManager.LoadSceneAsync("Level2");
            }
            else
            {
                SceneManager.LoadSceneAsync("MainMenu");
            }
            Destroy(this.gameObject);
        }

    }
}
