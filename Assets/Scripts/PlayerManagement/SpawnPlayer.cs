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
        if(direction=="right")
        {
            rightPlayer.SetActive(true);
        }
        else
        {
            leftPlayer.SetActive(true);
        }
    }

   
}
