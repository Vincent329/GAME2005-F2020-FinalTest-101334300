using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUpdate : MonoBehaviour
{
    public string beforeText;

    public TextMeshProUGUI thisText;
    public float tempVal;


    void Start()
    {
        thisText = GetComponent<TextMeshProUGUI>();
        tempVal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //thisText.text = "My text has now changed.";
    }

    public void ChangeText(float value)
    {
        tempVal = value;
        thisText.text = beforeText + tempVal.ToString("F2");
    }
}
