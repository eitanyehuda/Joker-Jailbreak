using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public bool isOnJoker = false;
    public int wall;
    public char suit;
    public int value;

    public bool faceUp = false;
    public bool selected = false;

    private string valueString;
    // Start is called before the first frame update
    void Start()
    {
        if (CompareTag("Card"))
        {
            suit = transform.name[name.Length-1];

            valueString = transform.name[0].ToString();
            if (transform.name.Length == 3)
            {
                valueString = valueString + transform.name[1].ToString();
            }

            if (valueString == "A")
            {
                value = 1;
            }
            else if (valueString == "2")
            {
                value = 2;
            }
            else if (valueString == "3")
            {
                value = 3;
            }
            else if (valueString == "4")
            {
                value = 4;
            }
            else if (valueString == "5")
            {
                value = 5;
            }
            else if (valueString == "6")
            {
                value = 6;
            }
            else if (valueString == "7")
            {
                value = 7;
            }
            else if (valueString == "8")
            {
                value = 8;
            }
            else if (valueString == "9")
            {
                value = 9;
            }
            else if (valueString == "10")
            {
                value = 10;
            }
            else if (valueString == "J")
            {
                value = 11;
            }
            else if (valueString == "Q")
            {
                value = 12;
            }
            else if (valueString == "K")
            {
                value = 13;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
