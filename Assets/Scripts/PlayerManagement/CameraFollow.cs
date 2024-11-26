using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothNess = 2f;

    // Update is called once per frame
    void Update()
    {
       if(player==null)
        {
            return;
        }
        Vector3 targetPos = new Vector3(player.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position,targetPos, smoothNess*Time.deltaTime );
    }
}
