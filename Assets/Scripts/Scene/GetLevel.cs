using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLevel : SingletonMono<GetLevel>
{
    public int LevelIndex;
    
    protected override void Awake()
    {
        // 调用基类的 Awake 方法
        base.Awake();
       
    }
   
    public int GetIndexOfLevel()
    {
        return LevelIndex;
    }


}
