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
            else //�ұ߻��ߴ����������Ĺ̶����ɵ㶼����ߣ������ֻ��һ����ڣ������ڵ����ɵ�λ����һ����
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
