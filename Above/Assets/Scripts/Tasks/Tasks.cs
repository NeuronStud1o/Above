using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tasks : MonoBehaviour
{
    public int jumps;
    public int gamesPlayed;
    public int rewards;

    public Button[] DoneGO;
    public Slider[] tasksPlayed;
    public Text[] resultText;

    void Update()
    {
        gamesPlayed = PlayerPrefs.GetInt("GamesPlayed");
        jumps = PlayerPrefs.GetInt("Jumps");
        rewards = PlayerPrefs.GetInt("RewardsCount");

        Jumps10000();
        
        PlayGame100();

        Buy5();

        Reward40();
        
        Boost1();

        PointsWithOneGame500();
        
        Jumps5000();
        
        SuperCoins10();

        EXP10();

        EveryDayTasks10();
    }

    void Jumps10000()
    {
        tasksPlayed[0].value = jumps;

        if (jumps >= 10000)
        {
            resultText[0].GetComponent<TextManager>().enabled = true;
            DoneGO[0].interactable = true;
        }
        else
        {
            resultText[0].text = jumps + " / 10000";
            DoneGO[0].interactable = false;
        }
    }

    void PlayGame100()
    {
        tasksPlayed[1].value = gamesPlayed;

        if (gamesPlayed >= 100)
        {
            resultText[1].GetComponent<TextManager>().enabled = true;
            DoneGO[1].interactable = true;
        }
        else
        {
            resultText[1].text = gamesPlayed + " / 100";
            DoneGO[1].interactable = false;
        }
    }

    void Buy5()
    {
        tasksPlayed[2].value = PlayerPrefs.GetInt("BoughtItems");

        if (PlayerPrefs.GetInt("BoughtItems") >= 5)
        {
            resultText[2].GetComponent<TextManager>().enabled = true;
            DoneGO[2].interactable = true;
        }
        else
        {
            resultText[2].text = PlayerPrefs.GetInt("BoughtItems") + " / 5";
            DoneGO[2].interactable = false;
        }
    }

    void Reward40()
    {
        tasksPlayed[3].value = rewards;

        if (rewards >= 40)
        {
            resultText[3].GetComponent<TextManager>().enabled = true;
            DoneGO[3].interactable = true;
        }
        else
        {
            DoneGO[3].interactable = false;
            resultText[3].text = rewards + " / 40";
        }
    }

    void Boost1()
    {
        tasksPlayed[4].value = PlayerPrefs.GetInt("BoostIsEquiped");

        if (PlayerPrefs.GetInt("BoostIsEquiped") > 0)
        {
            resultText[4].GetComponent<TextManager>().enabled = true;
            DoneGO[4].interactable = true;
        }
        else
        {
            DoneGO[4].interactable = false;
            resultText[4].text = 0 + " / 1";
        }
    }

    void PointsWithOneGame500()
    {
        tasksPlayed[5].value = PlayerPrefs.GetInt("recordScore");

        if (PlayerPrefs.GetInt("recordScore") >= 500)
        {
            resultText[5].GetComponent<TextManager>().enabled = true;
            DoneGO[5].interactable = true;
        }
        else
        {
            DoneGO[5].interactable = false;
            resultText[5].text = PlayerPrefs.GetInt("recordScore") + " / 500";
        }
    }

    void Jumps5000()
    {
        tasksPlayed[6].value = jumps;

        if (tasksPlayed[6].value >= 5000)
        {
            resultText[6].GetComponent<TextManager>().enabled = true;
            DoneGO[6].interactable = true;
        }
        else
        {
            resultText[6].text = jumps + " / 5000";
            DoneGO[6].interactable = false;
        }
    }

    void SuperCoins10()
    {
        if (PlayerPrefs.GetInt("coinsSForTasks") > 0)
        {
            resultText[7].GetComponent<TextManager>().enabled = true;
            DoneGO[7].interactable = true;
        }
        else
        {
            tasksPlayed[7].value = PlayerPrefs.GetInt("coinsS");
            resultText[7].text = PlayerPrefs.GetInt("coinsS") + " / 10";
            DoneGO[7].interactable = false;
        }
    }

    void EXP10()
    {
        tasksPlayed[8].value = PlayerPrefs.GetInt("LevelEXP");
        if (PlayerPrefs.GetInt("LevelEXP") >= 10)
        {
            resultText[8].GetComponent<TextManager>().enabled = true;
            DoneGO[8].interactable = true;
        }
        else
        {
            resultText[8].text = PlayerPrefs.GetInt("LevelEXP") + " / 10";
            DoneGO[8].interactable = false;
        }
    }

    void EveryDayTasks10()
    {
        tasksPlayed[9].value = PlayerPrefs.GetInt("EveryDayTasksDone");
        if (PlayerPrefs.GetInt("EveryDayTasksDone") >= 10)
        {
            resultText[9].GetComponent<TextManager>().enabled = true;
            DoneGO[9].interactable = true;
        }
        else
        {
            resultText[9].text = PlayerPrefs.GetInt("EveryDayTasksDone") + " / 10";
            DoneGO[9].interactable = false;
        }
    }
}
