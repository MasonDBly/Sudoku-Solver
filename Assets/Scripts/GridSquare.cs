using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridSquare : MonoBehaviour
{

    public GameObject number;
    private int value_ = 0;

    private List<int> possibleValues_ = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayText()
    {
        if (value_ != 0)
        {
            number.GetComponent<TextMeshProUGUI>().text = value_.ToString();
        }
        else
        {
            number.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void SetValue(int value)
    {
        value_ = value;
        DisplayText();
    }

    public void PropogateWFC()
    {

    }
}
