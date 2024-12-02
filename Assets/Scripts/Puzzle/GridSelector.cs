using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GridSelector : MonoBehaviour
{
    public  RectTransform gridParent; // Parent object with the Grid Layout Group
    public  GameObject frame;         // The frame that highlights the selected object
    public GameObject emptySpace;    // Reference to the empty space object

    private static RectTransform[] gridItems; // Array of all child objects in the grid
    public static int selectedIndex;         // Index of the currently selected object
    private int columns = 3;           // Number of columns in the grid
    private  bool isObjectSelected = false; // Whether an object is currently selected

    private  Transform[] originalOrder; // Stores the original order of the child objects

    public bool isSmallLevelOn = false;

    public Button ResetButton;
    public Button ReturnButton;
    public Button ReturnSmallMap;

    private void Start()
    {
        // Initialize the grid items from the children of the gridParent
        gridItems = new RectTransform[gridParent.childCount];
        originalOrder = new Transform[gridParent.childCount];

        for (int i = 0; i < gridParent.childCount; i++)
        {
            gridItems[i] = gridParent.GetChild(i).GetComponent<RectTransform>();
            originalOrder[i] = gridParent.GetChild(i); // Save the original order
        }

        // Start by selecting the first object
        selectedIndex = 0;
        AttachFrameToSelectedObject();

        //PrintGrid();
        //GetData.Instance.getGridItems = (RectTransform[])gridItems.Clone();

        //LoadFromSingleton();
        PrintGrid();
        ResetButton.onClick.AddListener(OnResetButtonClick);
        ReturnButton.onClick.AddListener(OnReturnButtonClick);
        ReturnSmallMap.onClick.AddListener(OnReturnSmallMapClick);
    }

    private void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (isSmallLevelOn)
        {          
            return;
        }

        if (isObjectSelected)
        {
            // Move the selected object if it's adjacent to the empty space
            if (Input.GetKeyDown(KeyCode.W)) TryMoveObject(Vector2Int.down);
            if (Input.GetKeyDown(KeyCode.S)) TryMoveObject(Vector2Int.up);
            if (Input.GetKeyDown(KeyCode.A)) TryMoveObject(Vector2Int.left);
            if (Input.GetKeyDown(KeyCode.D)) TryMoveObject(Vector2Int.right);
        }
        else
        {
            // Navigate with WASD
            if (Input.GetKeyDown(KeyCode.W)) MoveSelection(-columns); // Up
            if (Input.GetKeyDown(KeyCode.S)) MoveSelection(columns);  // Down
            if (Input.GetKeyDown(KeyCode.A)) MoveSelection(-1);       // Left
            if (Input.GetKeyDown(KeyCode.D)) MoveSelection(1);        // Right
        }

        // Press Space to select or deselect an object
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleObjectSelection();
        }

        // Press R to reset the grid to its original state
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ResetGrid();
        }
    }

    void OnResetButtonClick()
    {
        ResetGrid();
    }
    void OnReturnButtonClick()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }
    void OnReturnSmallMapClick()
    {
        SceneManager.LoadSceneAsync(GetNameByIndex(selectedIndex));
        //SceneManager.LoadSceneAsync(ScenesManager.Instance.playerInScene);
    }

    void MoveSelection(int offset)
    {
        int newIndex = selectedIndex + offset;

        // Check if the new index is valid
        if (newIndex >= 0 && newIndex < gridItems.Length)
        {
            // Check for valid horizontal movement
            if (Mathf.Abs(offset) == 1 && selectedIndex / columns != newIndex / columns)
            {
                // Prevent wrapping between rows
                return;
            }

            selectedIndex = newIndex;
            AttachFrameToSelectedObject();
        }
    }

    public  void AttachFrameToSelectedObject()
    {
        // Attach the frame to the currently selected object
        if (selectedIndex >= 0 && selectedIndex < gridItems.Length)
        {
            frame.transform.SetParent(gridItems[selectedIndex], false);
            frame.transform.localPosition = Vector3.zero; // Center the frame on the object
        }
    }

    void ToggleObjectSelection()
    {
        isObjectSelected = !isObjectSelected;

        // Change the frame's color to indicate selection
        frame.GetComponent<Renderer>().material.color = isObjectSelected ? Color.red : Color.white;
    }

    void TryMoveObject(Vector2Int direction)
    {
        // Calculate the new position in the grid
        int emptyIndex = System.Array.IndexOf(gridItems, emptySpace.transform);
        int targetIndex = selectedIndex + direction.x + direction.y * columns;

        // Ensure the target index is valid
        if (targetIndex < 0 || targetIndex >= gridItems.Length)
        {
            return;
        }

        // Check if the target index is the empty space
        if (targetIndex == emptyIndex)
        {
            // Swap the positions of the selected object and the empty space
            Vector3 tempPosition = gridItems[selectedIndex].position;
            gridItems[selectedIndex].position = gridItems[emptyIndex].position;
            gridItems[emptyIndex].position = tempPosition;

            // Swap their positions in the array
            RectTransform temp = gridItems[selectedIndex];
            gridItems[selectedIndex] = gridItems[emptyIndex];
            gridItems[emptyIndex] = temp;

            // Deselect the object
            ToggleObjectSelection();
            AttachFrameToSelectedObject();
            PrintGrid();

          //  GetData.Instance.getGridItems = (RectTransform[])gridItems.Clone();  //把数据传给GetData单例模式
        }
    }

    public  void ResetGrid()
    {
        // Clear the grid parent of all children
        for (int i = gridParent.childCount - 1; i >= 0; i--)
        {
            gridParent.GetChild(i).SetParent(null, false);
        }

        // Reattach the children in their original order
        foreach (Transform original in originalOrder)
        {
            original.SetParent(gridParent, false);
        }

        // Ensure the frame is reattached to the first object
        selectedIndex = 0;
        isObjectSelected = false;
        AttachFrameToSelectedObject();
    }

    public  static int GetIndexByName(string name)
    {
        Debug.Log("GetIndexByName:"+ name);
        for (int i = 0; i < gridItems.Length; i++)
        {
            if (gridItems[i] == null)
                Debug.LogError("gridItems " + i + " is null");
            if (gridItems[i] != null && gridItems[i].name == name)
            {
                return i;
            }
        }
        return -1; // Return -1 if the item is not found
    }

    public static string GetNameByIndex(int index)
    {
        if (index >= 0 && index < gridItems.Length)
        {
            return gridItems[index] != null ? gridItems[index].name : "Empty";
        }
        return "Invalid Index"; // Return an error message for invalid indices
    }


    public void PrintGrid()
    {
        for (int i = 0; i < gridItems.Length; i++)
        {
            // Ensure the grid item exists before accessing its name
            string itemName = gridItems[i] != null ? gridItems[i].name : "Empty";
          //  Debug.Log($"Index: {i}, Item Name: {itemName}");
        }
    }

    public void SaveToSingleton()
    {
        GridManager.Instance.gridItemNames = new string[gridItems.Length];
        for (int i = 0; i < gridItems.Length; i++)
        {
            GridManager.Instance.gridItemNames[i] = gridItems[i] != null ? gridItems[i].name : "Empty";
        }
    }

    public void LoadFromSingleton()
    {
        if (GridManager.Instance != null && GridManager.Instance.gridItemNames != null)
        {
            for (int i = 0; i < GridManager.Instance.gridItemNames.Length; i++)
            {
                if (GridManager.Instance.gridItemNames[i] == "Empty")
                {
                    gridItems[i] = null;
                }
                else
                {
                    GameObject obj = GameObject.Find(GridManager.Instance.gridItemNames[i]);
                    if (obj != null)
                    {
                        // Set the object in the correct position in the grid hierarchy
                        gridItems[i] = obj.GetComponent<RectTransform>();
                        obj.transform.SetParent(gridParent, false);

                        // Optionally reset the local position to ensure alignment
                        obj.transform.localPosition = new Vector3(
                            (i % columns) * gridParent.rect.width / columns,
                            -(i / columns) * gridParent.rect.height / columns,
                            0
                        );
                    }
                }
            }

            AttachFrameToSelectedObject(); // Reattach the frame to the selected item
        }
    }

    public string GetSelectedObjectName()
    {
        // Ensure the selected index is valid
        if (selectedIndex >= 0 && selectedIndex < gridItems.Length && gridItems[selectedIndex] != null)
        {
            return gridItems[selectedIndex].name;
        }
        return "None"; // Return "None" if no valid object is selected
    }




}
