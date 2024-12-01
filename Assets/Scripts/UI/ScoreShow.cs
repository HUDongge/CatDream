using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreShow : MonoBehaviour
{
    public  int currentCount = ScenesManager.Instance.currentScore;
    public List<Image> Collections; //有颜色的
    public List<Image> greyCollections;  //灰色的
    void Update()
    {
        if (ScenesManager.Instance.currentScore==1)
        {
            greyCollections[0].gameObject.SetActive(false);
            Collections[0].gameObject.SetActive(true);
        }
        else if(ScenesManager.Instance.currentScore == 2)
        {
            greyCollections[1].gameObject.SetActive(false);
            Collections[1].gameObject.SetActive(true);
        }
        else
        {
            greyCollections[0].gameObject.SetActive(true);
            greyCollections[1].gameObject.SetActive(true);
            Collections[0].gameObject.SetActive(false);
            Collections[1].gameObject.SetActive(false);
        }

    }
}
