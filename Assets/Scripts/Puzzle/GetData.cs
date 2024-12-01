using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetData : SingletonMono<GetData>
{

    public  RectTransform[] getGridItems= new RectTransform[9];
    private void Awake()
    {
        base.Awake();
        
    }
    private void Start()
    {
        PrintGrid();
        int temp=GetIndexByName("Level1_0");
        Debug.Log($"temp:{temp}");
    }

    public  int GetIndexByName(string name)
    {
        for (int i = 0; i < getGridItems.Length; i++)
        {
            if (getGridItems[i] == null)
            {
                Debug.LogError("hhhhh");
            }
                if (getGridItems[i] != null && getGridItems[i].name == name)
                {
                Debug.Log($"GetIndexByName:   " + i);
                return i;
               
                }
        }
        
        Debug.Log($"GetIndexByName:   " + -1);
        return -1; // Return -1 if the item is not found
    }

    public  string GetNameByIndex(int index)
    {      
        if (index >= 0 && index < getGridItems.Length)
        {
            return getGridItems[index] != null ? getGridItems[index].name : "Empty";
        }
        return "Invalid Index"; // Return an error message for invalid indices
    }

    public void PrintGrid()
    {
        for (int i = 0; i < getGridItems.Length; i++)
        {
            // Ensure the grid item exists before accessing its name
            string itemName = getGridItems[i] != null ? getGridItems[i].name : "Empty";
            Debug.Log($"IndexÊÇ: {i}, Item NameÊÇ: {itemName}");
        }
    }

}
