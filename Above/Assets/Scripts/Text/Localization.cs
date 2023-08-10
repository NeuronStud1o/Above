using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Localization : MonoBehaviour
{
   public void Ua()
    {
        SceneManager.LoadScene(0);
        string language = "Ua";
        PlayerPrefs.SetString("Language", language);
    }
    public void Eng()
    {
        SceneManager.LoadScene(0);
        string language = "Eng";
        PlayerPrefs.SetString("Language", language);
    }
    public void Den()
    {
        SceneManager.LoadScene(0);
        string language = "Den";
        PlayerPrefs.SetString("Language", language);
    }
    public void Fra()
    {
        SceneManager.LoadScene(0);
        string language = "Fra";
        PlayerPrefs.SetString("Language", language);
    }
    public void Cn()
    {
        SceneManager.LoadScene(0);
        string language = "Cn";
        PlayerPrefs.SetString("Language", language);
    }
    public void Es()
    {
        SceneManager.LoadScene(0);
        string language = "Es";
        PlayerPrefs.SetString("Language", language);
    }
    public void Ita()
    {
        SceneManager.LoadScene(0);
        string language = "Ita";
        PlayerPrefs.SetString("Language", language);
    }
    public void Pl()
    {
        SceneManager.LoadScene(0);
        string language = "Pl";
        PlayerPrefs.SetString("Language", language);
    }
}
