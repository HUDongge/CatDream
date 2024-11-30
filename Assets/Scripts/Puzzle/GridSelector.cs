using UnityEngine;

public class GridSelector : MonoBehaviour
{
    public RectTransform gridParent; // Parent object with the Grid Layout Group
    public GameObject frame;         // The frame that highlights the selected object
    public GameObject emptySpace;    // Reference to the empty space object

    private RectTransform[] gridItems; // Array of all child objects in the grid
    private int selectedIndex;         // Index of the currently selected object
    private int columns = 3;           // Number of columns in the grid
    private bool isObjectSelected = false; // Whether an object is currently selected

    private void Start()
    {
        // Initialize the grid items from the children of the gridParent
        gridItems = new RectTransform[gridParent.childCount];
        for (int i = 0; i < gridParent.childCount; i++)
        {
            gridItems[i] = gridParent.GetChild(i).GetComponent<RectTransform>();
        }

        // Start by selecting the first object
        selectedIndex = 0;
        AttachFrameToSelectedObject();
    }

    private void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
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

    void AttachFrameToSelectedObject()
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
            //isObjectSelected = false;
            ToggleObjectSelection();
            AttachFrameToSelectedObject();
        }
    }
}
