using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject rightPlayer;
    public GameObject leftPlayer;

    private void Start()
    {
        if(ScenesManager.Instance.isFirstCome)
        {
            ScenesManager.Instance.isFirstCome = false;
            leftPlayer.SetActive(true);
            rightPlayer.SetActive(false);
            return;
        }
        string selectScene = GridSelector.GetNameByIndex(GridSelector.selectedIndex);
        string playerInScene = ScenesManager.Instance.playerInScene;
       // Debug.LogError($"selectScene:{selectScene}"+$"playerInScene:{playerInScene}");
        if (playerInScene == selectScene)
        {
            leftPlayer.SetActive(true);
            rightPlayer.SetActive(false);
            return;
        }
        string direction = ScenesManager.Instance.Direction;
        if (direction != null)
        {
            if (direction == "left")
            {
                rightPlayer.SetActive(true);
                leftPlayer.SetActive(false);
            }
            else //右边或者从上面下来的固定生成点都是左边，如果是只有一个入口，场景摆地生成点位置是一样的
            {
                leftPlayer.SetActive(true);
                rightPlayer.SetActive(false);
            }
        }
        else
        {
            leftPlayer.SetActive(true);
            rightPlayer.SetActive(false);
        }
    }
    // Start is called before the first frame update
    
}
