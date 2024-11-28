using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemStone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
            UIManagement.currentCount++;           
            Destroy(this.gameObject);
        }

    }
}
