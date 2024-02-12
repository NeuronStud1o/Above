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
        infoPanel.SetActive(true);
        
        TextManagerTMP scriptText = infoText.GetComponent<TextManagerTMP>();
        scriptText.language = PlayerPrefs.GetString("Language");

        switch (scriptText.language)
        {
            case "Eng":
                scriptText.tmp.text = description.textEng;
                break;
            case "Ua":
                scriptText.tmp.text = description.textUa;
                break;
            case "Den":
                scriptText.tmp.text = description.textDen;
                break;
            case "Fra":
                scriptText.tmp.text = description.textFra;
                break;
            case "Es":
                scriptText.tmp.text = description.textEs;
                break;
            case "Ita":
                scriptText.tmp.text = description.textIta;
                break;
            case "Pl":
                scriptText.tmp.text = description.textPl;
                break;
            default:
                scriptText.tmp.text = description.textEng;
                break;
        }
        

        foreach (GameObject go in anotherAttributions)
        {
            go.SetActive(true);
        }

    }
}
