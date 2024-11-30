using UnityEngine;

public class OrganizeObjectsInGrid : MonoBehaviour
{
    public GameObject[] objects; // Drag and drop the GameObjects into this array in the Inspector
    public int rows = 3;         // Number of rows
    public int columns = 3;      // Number of columns
    public float spacing = 2f;   // Spacing between objects

    void Start()
    {
        OrganizeGrid();
    }

    void OrganizeGrid()
    {
        if (objects.Length == 0)
        {
            Debug.LogWarning("No objects assigned to organize.");
            return;
        }

        // Check if we have enough objects to fill the grid
        if (objects.Length < rows * columns)
        {
            Debug.LogWarning("Not enough objects to fill the grid!");
        }

        int index = 0; // Keep track of the current object to position
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                if (index >= objects.Length)
                    return;

                // Calculate the position for each object
                Vector3 position = new Vector3(col * spacing, row * -spacing, 0);

                // Assign the calculated position to the object
                objects[index].transform.position = position;
                index++;
            }
        }
    }
}
