using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public int columns = 9;
    public int rows = 9;
    public float squareOffset = 5.0f;
    public Vector2 startPosOffset = new Vector2(0.0f, 0.0f);


    public GameObject gridSquare;


    private List<GameObject> grid_squares_ = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (gridSquare.GetComponent<GridSquare>() == null)
        {
            Debug.LogError("Grid square does not have a Grid_Square script attached to it");
        }
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateGrid()
    {
        SpawnGridSquares();
        //SetGridSquares();
    }

    private void SpawnGridSquares()
    {
        var square_rect = gridSquare.GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        offset.x = square_rect.rect.width * square_rect.transform.localScale.x + squareOffset;
        offset.y = square_rect.rect.height * square_rect.transform.localScale.y + squareOffset;


        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                GameObject grid_square_instance = Instantiate(gridSquare, transform);
                grid_square_instance.transform.SetParent(transform);
                grid_square_instance.transform.localPosition = new Vector3(column
                    * offset.x
                    + startPosOffset.x, startPosOffset.y - row * offset.y, 0.0f);


                grid_squares_.Add(grid_square_instance);
            }
        }
    }

    // update the wave function collapse
    public void UpdateGrid(GridSquare gridSquare)
    {
        int value = gridSquare.GetValue();
        int pos = grid_squares_.IndexOf(gridSquare.gameObject);

        // for each grid sqare in the column
        for (int i = pos % columns; i < grid_squares_.Count; i += columns)
        {
            CheckGridSquare(i, value);
        }

        // for each grid square in the row
        for (int i = pos - pos % columns; i < grid_squares_.Count; i++)
        {
            CheckGridSquare(i, value);
        }

        // for each grid square in the 3x3 box
         
        
        return;
    }

    // check the gridsquare and propogate
    public void CheckGridSquare(int pos, int value)
    {
        GridSquare square = grid_squares_[pos].GetComponent<GridSquare>();
        if (square.GetValue() == 0 && square.ContainsValue(value))
        {
            square.RemoveValue(value);
            UpdateGrid(square);
        }
    }

    private bool IsValidSudokuValue(int value, int pos)
    {
        // check column
        for (int i = 0; i < grid_squares_.Count; i++)
        {
            if (i != pos && grid_squares_[i].GetComponent<GridSquare>().GetValue() == value)
            {
                return false;
            }
        }

        // check row
        for (int i = 0; i < grid_squares_.Count; i += columns)
        {
            if (i != pos && grid_squares_[i].GetComponent<GridSquare>().GetValue() == value)
            {
                return false;
            }
        }

        // check box
        int box_start = (pos / columns) / 3 * 3 * columns;
        int box_end = box_start + 3 * columns;
        for (int i = box_start; i < box_end; i++)
        {
            if (i != pos && grid_squares_[i].GetComponent<GridSquare>().GetValue() == value)
            {
                return false;
            }
        }

        return true;
    }

}


//private void PositionGridSquares()
//{
//    var square_rect = grid_squares_[0].GetComponent<RectTransform>();
//    Vector2 offset = new Vector2();
//    offset.x = square_rect.rect.width * square_rect.transform.localScale.x + square_offset;
//    offset.y = square_rect.rect.height * square_rect.transform.localScale.y + square_offset;

//    foreach (GameObject grid_square in grid_squares_)
//    {
//        grid_square.transform.localPosition = new Vector3(grid_square.transform.localPosition.x, grid_square.transform.localPosition.y, 0);
//    }
//}
