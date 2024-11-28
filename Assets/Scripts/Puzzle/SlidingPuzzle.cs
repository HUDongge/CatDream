using UnityEngine;

public class SlidingPuzzle : MonoBehaviour
{
    public GameObject[] puzzlePieces; // Array to hold the 8 puzzle pieces
    public GameObject emptyCell;      // The empty cell
    public GameObject selectionBox;   // The selection box (visual indicator)

    private GameObject selectedPiece = null; // Currently selected piece

    void Update()
    {
        HandleInput();
    }

    // Handle player input
    void HandleInput()
    {
        // Move selection box with WASD
        MoveSelectionBox();

        // Toggle selection with Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (selectedPiece == null)
            {
                SelectPiece();
            }
            else
            {
                MoveSelectedPiece();
            }
        }
    }

    // Move the selection box with WASD
    void MoveSelectionBox()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W)) direction = Vector3.up;
        if (Input.GetKeyDown(KeyCode.S)) direction = Vector3.down;
        if (Input.GetKeyDown(KeyCode.A)) direction = Vector3.left;
        if (Input.GetKeyDown(KeyCode.D)) direction = Vector3.right;

        // Update the selection box position
        if (direction != Vector3.zero)
        {
            selectionBox.transform.position += direction;
        }
    }

    // Select the piece at the current selection box position
    void SelectPiece()
    {
        foreach (var piece in puzzlePieces)
        {
            if (piece.transform.position == selectionBox.transform.position)
            {
                selectedPiece = piece;
                Debug.Log("Selected: " + piece.name);
                break;
            }
        }
    }

    // Move the selected piece to the empty cell
    void MoveSelectedPiece()
    {
        if (selectedPiece != null && IsAdjacentToEmptyCell(selectedPiece))
        {
            // Swap positions
            Vector3 emptyPosition = emptyCell.transform.position;
            emptyCell.transform.position = selectedPiece.transform.position;
            selectedPiece.transform.position = emptyPosition;

            Debug.Log("Moved: " + selectedPiece.name);

            // Deselect the piece
            selectedPiece = null;
        }
        else
        {
            Debug.Log("Selected piece is not adjacent to the empty cell!");
        }
    }

    // Check if the selected piece is adjacent to the empty cell
    bool IsAdjacentToEmptyCell(GameObject piece)
    {
        Vector3 piecePos = piece.transform.position;
        Vector3 emptyPos = emptyCell.transform.position;

        return Mathf.Abs(piecePos.x - emptyPos.x) + Mathf.Abs(piecePos.y - emptyPos.y) == 1;
    }
}
