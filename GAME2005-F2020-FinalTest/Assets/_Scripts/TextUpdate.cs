using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUpdate : MonoBehaviour
{
    public string beforeText;

    public TextMeshProUGUI thisText;


    void Start()
    {
        thisText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //thisText.text = "My text has now changed.";
    }

    public void ChangeText(float value)
    {
        thisText.text = beforeText + value.ToString("F2");
    }
}
