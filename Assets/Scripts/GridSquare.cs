using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public void DisplayText()
    {
        if (value_ != 0)
        {
            number.GetComponent<TextMeshProUGUI>().text = value_.ToString();
            possibleValues_.Clear();
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
