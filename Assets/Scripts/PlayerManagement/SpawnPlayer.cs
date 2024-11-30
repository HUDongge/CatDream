using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject rightPlayer;
    public GameObject leftPlayer;

    // Start is called before the first frame update
    void Start()
    {
       string direction = ScenesManager.Instance.Direction;
       // Debug.Log(direction);
         if(direction == "left")
        {
            rightPlayer.SetActive(true);
        }
        else //右边或者从上面下来的固定生成点都是左边，如果是只有一个入口，场景摆地生成点位置是一样的
        {
            leftPlayer.SetActive(true);
        }
    }

   
}
