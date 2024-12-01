using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject rightPlayer;
    public GameObject leftPlayer;

    // Start is called before the first frame update
    void Start()
    {
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
      //  SceneManager.LoadSceneAsync("Level" + ScenesManager.Instance.currentLevelIndex, LoadSceneMode.Additive);

    }

   
}
