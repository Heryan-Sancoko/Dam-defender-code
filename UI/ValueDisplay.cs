using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ValueDisplay : MonoBehaviour {

    Text text;
    public string prefix = "value: ";
    public string suffix = "";


    void Awake()
    {
        text = GetComponent<Text>();

    }

    public void SetText(int val)
    {
        if(text != null)
        text.text = prefix + val + suffix;
    }
    public void SetText(string val)

    {
        if (text != null)

            text.text = prefix + val + suffix;
    }
    public void SetText(float val)
    {
        if (text != null)

            text.text = prefix + val + suffix;
    }
}
