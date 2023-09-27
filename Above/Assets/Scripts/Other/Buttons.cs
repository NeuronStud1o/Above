using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public GameObject PlayTransition;
    public GameObject FadeTransition;

    public GameObject Hero;

    public GameObject settingsSounds;
    public GameObject settingsLanguages;

    public GameObject NeedWindow;
    public GameObject NeedWindow2;

    public GameObject[] CloseGameObject;

    public GameObject darkPanel;

    public Camera mainCamera;

    private void Start()
    {
        PlayTransition.SetActive(false);
    }

    // PAUSE CONTROLLER

    public void PauseGame()
    {
        Time.timeScale = 0;
        Hero.GetComponent<Player>().enabled = false;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        Hero.GetComponent<Player>().enabled = true;
    }

    // COROUTINES

    IEnumerator WaitForSecondsExample1()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator WaitForSecondsExample2()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

    IEnumerator WaitForSecondsExample3()
    {
        FadeTransition.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }

    IEnumerator WaitForSecondsExample4(GameObject window)
    {
        yield return new WaitForSeconds(0.3f);
        window.SetActive(false);
    }

    // MAIN MENU

    public void PlayGame()
    {
        int GamesPlayed = PlayerPrefs.GetInt("GamesPlayed");
        GamesPlayed++;
        PlayerPrefs.SetInt("GamesPlayed", GamesPlayed);
        PlayTransition.SetActive(true);
        StartCoroutine(WaitForSecondsExample1());

        if (ProgressEveryDayTasks.gamesPlayed != 0)
        {
            int played = PlayerPrefs.GetInt("EveryDayTasksGamesPlayed");
            played++;
            PlayerPrefs.SetInt("EveryDayTasksGamesPlayed", played);
        }
    }

    public void ExitButton()
    {
        NeedWindow.GetComponent<Animator>().SetTrigger("Exit");
        StartCoroutine(WaitForSecondsExample4(NeedWindow));
    }
    public void ExitButton2()
    {
        NeedWindow2.GetComponent<Animator>().SetTrigger("Exit");
        StartCoroutine(WaitForSecondsExample4(NeedWindow2));
    }

    public void RetryGame()
    {
        int GamesPlayed = PlayerPrefs.GetInt("GamesPlayed");
        GamesPlayed++;
        PlayerPrefs.SetInt("GamesPlayed", GamesPlayed);
        PlayTransition.SetActive(true);
        StartCoroutine(WaitForSecondsExample2());

        if (ProgressEveryDayTasks.gamesPlayed != 0)
        {
            int played = PlayerPrefs.GetInt("EveryDayTasksGamesPlayed");
            played++;
            PlayerPrefs.SetInt("EveryDayTasksGamesPlayed", played);
        }
    }

    public void MainGame()
    {
        StartCoroutine(WaitForSecondsExample3());
    }

    // EVERY DAY TASKS

    public void AddEXP10EVERYDAYTASK()
    {
        EXPmanager.countEXP += 10;
    }

    // TASKS

    public void AddEXP10(int index)
    {
        CloseGameObject[index].SetActive(true);
        EXPmanager.countEXP += 10;
    }

    public void AddEXP15(int index)
    {
        CloseGameObject[index].SetActive(true);
        EXPmanager.countEXP += 15;
    }

    public void AddEXP20(int index)
    {
        CloseGameObject[index].SetActive(true);
        EXPmanager.countEXP += 20;
    }

    public void Add1SuperCoins(int index)
    {
        CloseGameObject[index].SetActive(true);
        CoinsManagerInMainMenu.coinsS += 1;
        PlayerPrefs.SetInt("coinsS", CoinsManagerInMainMenu.coinsS);
    }

    public void Add30FlyCoins(int index)
    {
        CloseGameObject[index].SetActive(true);
        CoinsManagerInMainMenu.coinsF += 30;
        PlayerPrefs.SetInt("coinsF", CoinsManagerInMainMenu.coinsF);
    }

    public void Add50FlyCoins(int index)
    {
        CloseGameObject[index].SetActive(true);
        CoinsManagerInMainMenu.coinsF += 50;
        PlayerPrefs.SetInt("coinsF", CoinsManagerInMainMenu.coinsF);
    }

    // ADS

    public void ReturnToGame()
    {
        StartCoroutine(ScaleRect());
    }

    IEnumerator ScaleRect()
    {
        darkPanel.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        mainCamera.rect = new Rect(0f, -0.04f, 1f, 1f);

        yield return new WaitForSeconds(0.2f);
        darkPanel.SetActive(false);
    }

    //URL

    public void Instagram()
    {
        Application.OpenURL("https://instagram.com/neuron.studio.official?utm_source=qr&igshid=MzNlNGNkZWQ4Mg%3D%3D");
    }

    public void PrivacyPolicy()
    {
        Application.OpenURL("https://doc-hosting.flycricket.io/above-privacy-policy/78dbabd2-a6e1-4a4e-b924-9402c88f8348/privacy");
    }
}
    