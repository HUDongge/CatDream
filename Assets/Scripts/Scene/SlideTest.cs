using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideTest : MonoBehaviour
{
    //���ݳ���index���Ҷ�Ӧ��λ�ã���������λ��
    public static List<int> sceneToPos = new List<int>()
   {
       0,1,2  //����0��1��2�ھŹ���������λ�÷ֱ���0��1��2
   };
    //����λ���ҳ���
    public static List<string> PosToScene = new List<string>()
    {
        "Level1_1","Level1_2","Level1_3" //�ھŹ���������λ����0��1��2�Ŀ�ֱ��Ӧ����1��2,3
    };

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
