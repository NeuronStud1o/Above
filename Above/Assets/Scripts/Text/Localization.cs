using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Localization : MonoBehaviour
{
   public void Ua()
    {
        string language = "Ua";
        PlayerPrefs.SetString("Language", language);
    }
    public void Eng()
    {
        string language = "Eng";
        PlayerPrefs.SetString("Language", language);
    }
    public void Den()
    {
        string language = "Den";
        PlayerPrefs.SetString("Language", language);
    }
    public void Fra()
    {
        string language = "Fra";
        PlayerPrefs.SetString("Language", language);
    }
    public void Cn()
    {
        string language = "Cn";
        PlayerPrefs.SetString("Language", language);
    }
    public void Es()
    {
        string language = "Es";
        PlayerPrefs.SetString("Language", language);
    }
    public void Ita()
    {
        string language = "Ita";
        PlayerPrefs.SetString("Language", language);
    }
    public void Pl()
    {
        string language = "Pl";
        PlayerPrefs.SetString("Language", language);
    }
}
