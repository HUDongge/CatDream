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
    }

    public  int GetIndexByName(string name)
    {
        Debug.Log($"GetIndexByName:   "+name);

        for (int i = 0; i < getGridItems.Length; i++)
        {
            if (getGridItems[i] != null && getGridItems[i].name == name)
            {
                return i;
            }
        }
        return -1; // Return -1 if the item is not found
    }

    public  string GetNameByIndex(int index)
    {

        Debug.Log($"GetNameByIndex:   " + index);
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
