using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance;  // 私有静态实例
    public static T Instance { get { return instance; } }  // 公开实例属性

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
           
            DontDestroyOnLoad(gameObject);
        }
        else
        {
          
            Destroy(gameObject);
        }
    }
}
