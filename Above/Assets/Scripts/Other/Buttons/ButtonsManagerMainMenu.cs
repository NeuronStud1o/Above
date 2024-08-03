using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsManagerMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject shopWindow;
    [SerializeField] private GameObject settingsWindow;

    IEnumerator LoadGameSceneAsync()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadSceneAsync("Game");
    }

    IEnumerator ExitWindowAsync(GameObject window)
    {
        yield return new WaitForSeconds(0.3f);

        window.SetActive(false);
    }

    public void PlayGame()
    {
        GameManager.instance.gameObject.GetComponent<AudioSource>().enabled = false;
        GameManager.instance.SetActiveLoadingScreen(true);

        StartCoroutine(LoadGameSceneAsync());
    }

    public void ExitShop()
    {
        shopWindow.GetComponent<Animator>().SetTrigger("Exit");

        StartCoroutine(ExitWindowAsync(shopWindow));
    }

    public void ExitSettings()
    {
        settingsWindow.GetComponent<Animator>().SetTrigger("Exit");

        StartCoroutine(ExitWindowAsync(settingsWindow));
    }

    public void PrivacyPolicy()
    {
        Application.OpenURL("https://doc-hosting.flycricket.io/above-privacy-policy/b4629e4f-e735-4eda-83c7-0c667307bf57/privacy");
    }
}
