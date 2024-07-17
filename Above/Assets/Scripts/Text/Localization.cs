using UnityEngine;
using UnityEngine.SceneManagement;

public class Localization : MonoBehaviour
{
    public void ChangeLanguage(string language)
    {
        PlayerPrefs.SetString("Language", language);

        SceneManager.LoadSceneAsync("Authentication");
    }
}
