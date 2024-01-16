using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsManagerMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject playTransition;
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
        playTransition.SetActive(true);
        DataBase.instance.gameObject.GetComponent<AudioSource>().enabled = false;
        DataBase.instance.SetActiveLoadingScreen(true);

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

    public void Instagram()
    {
        Application.OpenURL("https://instagram.com/neuron.studio.official?utm_source=qr&igshid=MzNlNGNkZWQ4Mg%3D%3D");
    }

    public void PrivacyPolicy()
    {
        Application.OpenURL("https://doc-hosting.flycricket.io/above-privacy-policy/78dbabd2-a6e1-4a4e-b924-9402c88f8348/privacy");
    }
}
