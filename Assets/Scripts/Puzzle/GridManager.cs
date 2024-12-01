using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public string[] gridItemNames; // Stores the names of the grid items

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameObject across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }
}