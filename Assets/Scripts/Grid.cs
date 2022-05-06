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

    // Set all grid quares to a random int between 1 and 10
    private void SetGridSquares()
    {
        foreach (GameObject grid_square in grid_squares_)
        {
            grid_square.GetComponent<GridSquare>().SetValue(Random.Range(1, 10));
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
}
