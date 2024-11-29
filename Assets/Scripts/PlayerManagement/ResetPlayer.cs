using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    public float respawnDelay = 3f;  // 重生延迟时间

    void OnEnable()
    {

        PlayerController.OnPlayerDeath += HandlePlayerDeath;
    }

    void OnDisable()
    {
        PlayerController.OnPlayerDeath -= HandlePlayerDeath;
    }

    // 事件处理函数，接收玩家的位置
    void HandlePlayerDeath(Vector3 playerPosition)
    {
        StartCoroutine(Respawn(playerPosition));

    }

    IEnumerator Respawn(Vector3 playerPosition)
    {
        yield return new WaitForSeconds(respawnDelay);
        
        GameObject newPlayer=Instantiate(playerPrefab, playerPosition, Quaternion.identity);
      
    }

}
