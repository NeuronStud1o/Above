using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoAccIconPanel : MonoBehaviour
{
    [Header ("## Visual : ")]
    [SerializeField] private Sprite icon;
    [SerializeField] private TextLoc description;

    [Serializable]
    struct TextLoc
    {
        public string textUa;
        public string textEng;
        public string textDen;
        public string textFra;
        public string textEs;
        public string textIta;
        public string textPl;
    }

    [Header ("## Info panel elements : ")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private Image iconInfoPanel;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private List<GameObject> anotherAttributions;

    public void OpenInfoPanel()
    {
        iconInfoPanel.sprite = icon;
        
        TextManagerTMP scriptText = infoText.GetComponent<TextManagerTMP>();
        
        string language = scriptText.language;

        switch (language)
        {
            case "Eng":
                scriptText.text.text = description.textEng;
                break;
            case "Ua":
                scriptText.text.text = description.textUa;
                break;
            case "Den":
                scriptText.text.text = description.textDen;
                break;
            case "Fra":
                scriptText.text.text = description.textFra;
                break;
            case "Es":
                scriptText.text.text = description.textEs;
                break;
            case "Ita":
                scriptText.text.text = description.textIta;
                break;
            case "Pl":
                scriptText.text.text = description.textPl;
                break;
            default:
                scriptText.text.text = description.textEng;
                break;
        }
        

        foreach (GameObject go in anotherAttributions)
        {
            go.SetActive(true);
        }

        infoPanel.SetActive(true);
    }
}
