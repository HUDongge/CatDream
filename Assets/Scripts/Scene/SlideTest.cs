using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideTest : MonoBehaviour
{
    //根据场景index查找对应的位置，数组里存的位置
    public static List<int> sceneToPos = new List<int>()
   {
       0,1,2  //场景0，1，2在九宫格数组里位置分别是0，1，2
   };
    //根据位置找场景
    public static List<string> PosToScene = new List<string>()
    {
        "Level1_1","Level1_2","Level1_3" //在九宫格数组里位置是0，1，2的块分别对应场景1，2,3
    };

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
