using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressEveryDayTasks : MonoBehaviour
{
    public Button[] EveryDayTaskButtons;
    public Slider[] EveryDayTaskSlider;
    public Text[] EveryDayTaskText;

    public GameObject[] EveryDayTaskList;

    public static int points;
    public static int jumps;
    public static int gamesPlayed;
    public static int flyCoinsEarned;

    void Update()
    {
        if (EveryDayTaskList[0].activeSelf) 
        {
            points = 200;

            EveryDayTaskSlider[0].value = PlayerPrefs.GetInt("tasksRecordScore");

            if (PlayerPrefs.GetInt("tasksRecordScore") >= 200)
            {
                EveryDayTaskButtons[0].interactable = true;
                EveryDayTaskText[0].GetComponent<TextManager>().enabled = true;
            }
            else
            {
                EveryDayTaskText[0].text = PlayerPrefs.GetInt("tasksRecordScore") + " / 200";
            }
        }

        if (EveryDayTaskList[1].activeSelf)
        {
            jumps = 500;

            EveryDayTaskSlider[1].value = PlayerPrefs.GetInt("TasksJumps");

            if (PlayerPrefs.GetInt("TasksJumps") >= 500)
            {
                EveryDayTaskButtons[1].interactable = true;
                EveryDayTaskText[1].GetComponent<TextManager>().enabled = true;
            }
            else
            {
                EveryDayTaskText[1].text = PlayerPrefs.GetInt("TasksJumps") + " / 500";
            }
        }

        if (EveryDayTaskList[2].activeSelf)
        {
            points = 150;

            EveryDayTaskSlider[2].value = PlayerPrefs.GetInt("tasksRecordScore");

            if (PlayerPrefs.GetInt("tasksRecordScore") >= 150)
            {
                EveryDayTaskButtons[2].interactable = true;
                EveryDayTaskText[2].GetComponent<TextManager>().enabled = true;
            }
            else
            {
                EveryDayTaskText[2].text = PlayerPrefs.GetInt("tasksRecordScore") + " / 150";
            }
        }

        if (EveryDayTaskList[3].activeSelf)
        {
            gamesPlayed = 15;

            EveryDayTaskSlider[3].value = PlayerPrefs.GetInt("EveryDayTasksGamesPlayed");

            if (PlayerPrefs.GetInt("EveryDayTasksGamesPlayed") >= 15)
            {
                EveryDayTaskButtons[3].interactable = true;
                EveryDayTaskText[3].GetComponent<TextManager>().enabled = true;
            }
            else
            {
                EveryDayTaskText[3].text = PlayerPrefs.GetInt("EveryDayTasksGamesPlayed") + " / 15";
            }
        }

        if (EveryDayTaskList[4].activeSelf)
        {
            gamesPlayed = 10;

            EveryDayTaskSlider[4].value = PlayerPrefs.GetInt("EveryDayTasksGamesPlayed");

            if (PlayerPrefs.GetInt("EveryDayTasksGamesPlayed") >= 10)
            {
                EveryDayTaskButtons[4].interactable = true;
                EveryDayTaskText[4].GetComponent<TextManager>().enabled = true;
            }
            else
            {
                EveryDayTaskText[4].text = PlayerPrefs.GetInt("EveryDayTasksGamesPlayed") + " / 10";
            }
        }

        if (EveryDayTaskList[5].activeSelf)
        {
            flyCoinsEarned = 10;

            EveryDayTaskSlider[5].value = PlayerPrefs.GetInt("EveryDayTasksFlyCoinsEarned");

            if (PlayerPrefs.GetInt("EveryDayTasksFlyCoinsEarned") >= 10)
            {
                EveryDayTaskButtons[5].interactable = true;
                EveryDayTaskText[5].GetComponent<TextManager>().enabled = true;
            }
            else
            {
                EveryDayTaskText[5].text = PlayerPrefs.GetInt("EveryDayTasksFlyCoinsEarned") + " / 10";
            }
        }

        if (EveryDayTaskList[6].activeSelf)
        {
            jumps = 400;

            EveryDayTaskSlider[6].value = PlayerPrefs.GetInt("TasksJumps");

            if (PlayerPrefs.GetInt("TasksJumps") >= 400)
            {
                EveryDayTaskButtons[6].interactable = true;
                EveryDayTaskText[6].GetComponent<TextManager>().enabled = true;
            }
            else
            {
                EveryDayTaskText[6].text = PlayerPrefs.GetInt("TasksJumps") + " / 400";
            }
        }

        if (EveryDayTaskList[7].activeSelf)
        {
            points = 250;

            EveryDayTaskSlider[7].value = PlayerPrefs.GetInt("tasksRecordScore");

            if (PlayerPrefs.GetInt("tasksRecordScore") >= 250)
            {
                EveryDayTaskButtons[7].interactable = true;
                EveryDayTaskText[7].GetComponent<TextManager>().enabled = true;
            }
            else
            {
                EveryDayTaskText[7].text = PlayerPrefs.GetInt("tasksRecordScore") + " / 250";
            }
        }

        if (EveryDayTaskList[8].activeSelf)
        {
            jumps = 700;

            EveryDayTaskSlider[8].value = PlayerPrefs.GetInt("TasksJumps");

            if (PlayerPrefs.GetInt("TasksJumps") >= 700)
            {
                EveryDayTaskButtons[8].interactable = true;
                EveryDayTaskText[8].GetComponent<TextManager>().enabled = true;
            }
            else
            {
                EveryDayTaskText[8].text = PlayerPrefs.GetInt("TasksJumps") + " / 700";
            }
        }

        if (EveryDayTaskList[9].activeSelf)
        {
            gamesPlayed = 20;

            EveryDayTaskSlider[9].value = PlayerPrefs.GetInt("EveryDayTasksGamesPlayed");

            if (PlayerPrefs.GetInt("EveryDayTasksGamesPlayed") >= 20)
            {
                EveryDayTaskButtons[9].interactable = true;
                EveryDayTaskText[9].GetComponent<TextManager>().enabled = true;
            }
            else
            {
                EveryDayTaskText[9].text = PlayerPrefs.GetInt("EveryDayTasksGamesPlayed") + " / 20";
            }
        }
    }
}
