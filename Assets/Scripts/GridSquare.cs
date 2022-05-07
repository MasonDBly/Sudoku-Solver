using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GridSquare : MonoBehaviour
{

    public GameObject number;
    public GameObject button;
    private int value_ = 0;

    private List<int> possibleValues_ = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private List<GameObject> buttons = new();

    // Start is called before the first frame update
    void Start()
    {
        CreateButtons();
    }

    // Update is called once per frame
    void Update()
    {
        // delete each button whos value is not in possibleValues_
        for (int i = buttons.Count - 1; i >= 0; i--)
        {
            if (!possibleValues_.Contains(int.Parse(buttons[i].GetComponentInChildren<TextMeshProUGUI>().text)))
            {
                Destroy(buttons[i]);
                buttons.RemoveAt(i);
            }
        }

        if (possibleValues_.Count == 1)
        {
            SetValue(possibleValues_[0]);
        }
    }

    private void CreateButtons()
    {
        float offsetBetween = GetComponent<RectTransform>().rect.width / 3;
        float startingOffset = -offsetBetween * 1.0f;
        float x = startingOffset;
        float y = -startingOffset;
        foreach (int value in possibleValues_)
        {
            GameObject button = Instantiate(this.button, transform);
            button.transform.SetParent(transform);
            button.transform.localPosition = new Vector3(x, y, 0);
            button.GetComponentInChildren<TextMeshProUGUI>().text = value.ToString();
            buttons.Add(button);

            x += offsetBetween;
            if (x > offsetBetween)
            {
                x = startingOffset;
                y -= offsetBetween;
            }
        }
    }

    private void DisplayText()
    {
        if (value_ != 0)
        {
            number.GetComponent<TextMeshProUGUI>().text = value_.ToString();
            possibleValues_.Clear();
            GetComponentInParent<Grid>().UpdateGrid(this);
        }
        else
        {
            throw new Exception("No value set");
        }
    }

    public void SetValue(int value)
    {
        value_ = value;
        DisplayText();
    }

    public int GetValue()
    {
        return value_;
    }

    internal void RemoveValue(int value)
    {
        possibleValues_.Remove(value);
    }

    internal List<int> GetValues()
    {
        return possibleValues_;
    }

    internal bool ContainsValue(int value)
    {
        return possibleValues_.Contains(value);
    }
}
