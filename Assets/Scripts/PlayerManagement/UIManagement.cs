using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//这个是测试得！
public class UIManagement : MonoBehaviour
{

    public static int currentCount = 0;
    public List<Image> Collections; //有颜色的
    public List<Image> greyCollections;  //灰色的
    void Update()
    {
        if(currentCount>0)
        {
            greyCollections[currentCount - 1].gameObject.SetActive(false);
            Collections[currentCount - 1].gameObject.SetActive(true);
        }
        
    }
    

    
}
