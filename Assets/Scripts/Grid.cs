using System;
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
        if (grid_squares_.Count == 81)
        {
            CheckRows();
            CheckColumns();
            CheckBoxes();
        }
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
        int start = pos - pos % 9;
        for (int i = start; i < start + 9; i++)
        {
            CheckGridSquare(i, value);
        }

        // for each grid square in the 3x3 box
        int boxStart = GetBoxStart(pos);
        for (int i = boxStart; i < boxStart + 21; i++)
        {
            if (i % 3 == 0 && i != boxStart)
            {
                i += 6;
            }
            CheckGridSquare(i, value);
        }


        return;
    }


    // check the gridsquare and propogate
    public void CheckGridSquare(int pos, int value)
    {
        GridSquare square = grid_squares_[pos].GetComponent<GridSquare>();
        if (square.GetValue() == 0 && square.ContainsValue(value))
        {
            square.RemoveValue(value);
        }
    }

    private int GetBoxStart(int x)
    {
        return x - x % 3 - 9 * (x / 9) + 27 * (x / 27);
    }

    private void CheckColumns()
    {
        for (int col = 0; col < columns; col++)
        {
            var counts = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = col; i < grid_squares_.Count; i += columns)
            {
                GridSquare square = grid_squares_[i].GetComponent<GridSquare>();
                if (square.GetValue() != 0)
                {
                    counts[square.GetValue() - 1] = -1;
                }
                else
                {
                    foreach (int value in square.GetValues())
                    {
                        if (value != -1)
                        {
                            counts[value - 1]++;
                        }
                    }
                }
            }
            // if any of the counts are 1, set the possible grid square to that value
            for (int j = 0; j < counts.Count; j++)
            {
                if (counts[j] == 1)
                {
                    for (int i = col; i < grid_squares_.Count; i += columns)
                    {
                        GridSquare square = grid_squares_[i].GetComponent<GridSquare>();
                        if (square.GetValue() == 0 && square.ContainsValue(j + 1))
                        {
                            square.SetValue(j + 1);
                        }
                    }
                }
            }
        }
    }

    private void CheckRows()
    {
        for (int row = 0; row < rows; row++)
        {
            var counts = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = row * 9; i < (row + 1) * 9; i++)
            {
                GridSquare square = grid_squares_[i].GetComponent<GridSquare>();
                if (square.GetValue() != 0)
                {
                    counts[square.GetValue() - 1] = -1;
                }
                else
                {
                    foreach (int value in square.GetValues())
                    {
                        if (value != -1)
                        {
                            counts[value - 1]++;
                        }
                    }
                }
            }
            // if any of the counts are 1, set the possible grid square to that value
            for (int j = 0; j < counts.Count; j++)
            {
                if (counts[j] == 1)
                {
                    for (int i = row * 9; i < row * 10; i++)
                    {
                        GridSquare square = grid_squares_[i].GetComponent<GridSquare>();
                        if (square.GetValue() == 0 && square.ContainsValue(j + 1))
                        {
                            square.SetValue(j + 1);
                        }
                    }
                }
            }
        }
    }

    private void CheckBoxes()
    {
        for (int box = 0; box < 9; box++)
        {
            var counts = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int start = (box * 3) % 9 + (box / 3) * 27;
            for (int i = start; i < start + 21; i++)
            {
                if (i % 3 == 0 && i != start)
                {
                    i += 6;
                }
                GridSquare square = grid_squares_[i].GetComponent<GridSquare>();
                if (square.GetValue() != 0)
                {
                    counts[square.GetValue() - 1] = -1;
                }
                else
                {
                    foreach (int value in square.GetValues())
                    {
                        if (value != -1)
                        {
                            counts[value - 1]++;
                        }
                    }
                }
            }
            // if any of the counts are 1, set the possible grid square to that value
            for (int j = 0; j < counts.Count; j++)
            {
                if (counts[j] == 1)
                {
                    for (int i = start; i < start + 21; i++)
                    {
                        if (i % 3 == 0 && i != start)
                        {
                            i += 6;
                        }
                        GridSquare square = grid_squares_[i].GetComponent<GridSquare>();
                        if (square.GetValue() == 0 && square.ContainsValue(j + 1))
                        {
                            square.SetValue(j + 1);
                        }
                    }
                }
            }
        }
    }

    
}