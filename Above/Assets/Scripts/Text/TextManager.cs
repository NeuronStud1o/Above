using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{

    public string language;
    Text text;

    public string textUa;
    public string textEng;
    public string textDen;
    public string textFra;
    public string textCn;
    public string textEs;
    public string textIta;
    public string textPl;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        language = PlayerPrefs.GetString("Language");

        if(language == "" || language == "Eng")
        {
            text.text = textEng;
        }
        else if (language == "Ua")
        {
            text.text = textUa;
        }
        else if (language == "Den")
        {
            text.text = textDen;
        }
        else if (language == "Fra")
        {
            text.text = textFra;
        }
        else if (language == "Cn")
        {
            text.text = textCn;
        }
        else if (language == "Es")
        {
            text.text = textEs;
        }
        else if (language == "Ita")
        {
            text.text = textIta;
        }
        else if (language == "Pl")
        {
            text.text = textPl;
        }
    }
}
