using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLevel : SingletonMono<GetLevel>
{
    public int LevelIndex;
    
    protected override void Awake()
    {
        // ���û���� Awake ����
        base.Awake();
       
    }
   
    public int GetIndexOfLevel()
    {
        return LevelIndex;
    }


}
