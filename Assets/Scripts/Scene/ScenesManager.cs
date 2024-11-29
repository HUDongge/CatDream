using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    // ����ʵ��
    private static ScenesManager instance;

    // ��ȡ����ʵ��
    public static ScenesManager Instance
    {
        get
        {
            if (instance == null)
            {
                // ���Բ������е�ʵ��
                instance = FindObjectOfType<ScenesManager>();

                // �����û��ʵ��������һ���µ�ʵ��
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(ScenesManager).Name);
                    instance = singletonObject.AddComponent<ScenesManager>();
                    DontDestroyOnLoad(singletonObject);  // ȷ���ڳ����л�ʱ��������
                }
            }
            return instance;
        }
    }

    // �洢ÿ�����������״̬
    private Dictionary<string, SceneEntrance> sceneEntrances;

    public string Direction; //���ķ���

    private void Awake()
    {
        // ȷ��ֻ��һ��ʵ������
        if (instance != null && instance != this)
        {
            Destroy(gameObject);  // ����Ѿ���ʵ���ˣ������ٵ�ǰ����
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // ���ֳ������������л�����ʱ��������
        }

        // ��ʼ���������״̬����������
        sceneEntrances = new Dictionary<string, SceneEntrance>
        {
            { "Level1_1", new SceneEntrance(false, false, false, true) }, 
            { "Level1_2", new SceneEntrance(false, false, true, true) },
            { "Level1_3", new SceneEntrance(false, true, true, true) },
            { "Level1_4", new SceneEntrance(false, true, false, false) },
            { "Level1_5", new SceneEntrance(false, false, true, false) },
            { "Level1_6", new SceneEntrance(false, false, false, true) },
            { "Level1_7", new SceneEntrance(false, true, false, false) },
            { "Level1_8", new SceneEntrance(false, false, false, false) }
        };
    }

    // ��ȡ��ǰ���������״̬
    public SceneEntrance GetSceneEntrance(string sceneName)
    {
        if (sceneEntrances.ContainsKey(sceneName))
        {
            return sceneEntrances[sceneName];
        }
        else
        {
            Debug.LogError("Scene not found: " + sceneName);
            return null;
        }
    }

    // �л��������߼�
    public void SwitchScene(int currentSceneIndex,string direction )
    {
        int currentPos = SlideTest.sceneToPos[currentSceneIndex];//���ҵ�ǰ������ڳ������ھŹ������λ��
        if(direction=="right")
        {
            Debug.Log($"direction:{direction}");

            Direction = "right";
         
            if ((currentPos + 1)% 3 == 0)  //�ھŹ������ұ��ˣ�������������
            {
                return;
            }
            else if (sceneEntrances[SlideTest.PosToScene[currentPos+1]].canPassLeft)  //������������һ���ҵ��ø��Ӷ�Ӧ�ĳ���
            {
                SceneManager.LoadScene(SlideTest.PosToScene[currentPos + 1]);

            }    
        }
       


    }
   
}