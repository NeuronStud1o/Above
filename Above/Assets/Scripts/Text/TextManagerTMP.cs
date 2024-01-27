using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextManagerTMP : MonoBehaviour
{
    public string language;
    public TextMeshProUGUI text;

    public string textUa;
    public string textEng;
    public string textDen;
    public string textFra;
    public string textEs;
    public string textIta;
    public string textPl;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        ChooseLanguage();
    }

    public void ChooseLanguage()
    {
        language = PlayerPrefs.GetString("Language");

        switch (language)
        {
            case "Eng":
                text.text = textEng;
                break;
            case "Ua":
                text.text = textUa;
                break;
            case "Den":
                text.text = textDen;
                break;
            case "Fra":
                text.text = textFra;
                break;
            case "Es":
                text.text = textEs;
                break;
            case "Ita":
                text.text = textIta;
                break;
            case "Pl":
                text.text = textPl;
                break;
            default:
                text.text = textEng;
                break;
        }
    }
}
