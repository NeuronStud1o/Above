using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextManagerTMP : MonoBehaviour
{
    public string language;
    public TextMeshProUGUI tmp;

    public string textUa;
    public string textEng;
    public string textDen;
    public string textFra;
    public string textEs;
    public string textIta;
    public string textPl;

    void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        ChooseLanguage();
    }

    public void ChooseLanguage()
    {
        language = PlayerPrefs.GetString("Language");

        switch (language)
        {
            case "Eng":
                tmp.text = textEng;
                break;
            case "Ua":
                tmp.text = textUa;
                break;
            case "Den":
                tmp.text = textDen;
                break;
            case "Fra":
                tmp.text = textFra;
                break;
            case "Es":
                tmp.text = textEs;
                break;
            case "Ita":
                tmp.text = textIta;
                break;
            case "Pl":
                tmp.text = textPl;
                break;
            default:
                tmp.text = textEng;
                break;
        }
    }
}
