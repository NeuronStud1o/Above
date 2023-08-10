using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EveryDayTask : MonoBehaviour
{
    public float msToWait = 5000.0f;

    private Text Timer;
    public Button NewTaskButton;

    private ulong lastOpen;

    public GameObject Done;
    public GameObject MainDone;

    public GameObject[] Tasks;

    System.Random r = new System.Random();

    private int index;

    void Start()
    {
        PlayerPrefs.GetInt("isReadyForTask");
        if (!PlayerPrefs.HasKey("lastOpen2"))
        {
            PlayerPrefs.SetString("lastOpen2", "0");
        }
        lastOpen = ulong.Parse(PlayerPrefs.GetString("lastOpen2"));
        Timer = GetComponentInChildren<Text>();

        if (!isReady())
        {
            Done.SetActive(false);
            MainDone.SetActive(false);
            NewTaskButton.interactable = false;
        }
    }

    IEnumerator AnimationSeconds()
    {
        yield return new WaitForSeconds(2);
        int allCoins = PlayerPrefs.GetInt("coinsF");
        allCoins += 10;
        PlayerPrefs.SetInt("coinsF", allCoins);
    }

    void Update()
    {
        if (!NewTaskButton.IsInteractable())
        {
            if (isReady())
            {
                if (PlayerPrefs.GetInt("isReadyForTask") == 1)
                {
                    ChangeTask();
                }

                Tasks[PlayerPrefs.GetInt("TaskEquiped")].SetActive(true);

                Done.SetActive(true);
                MainDone.SetActive(true);

                NewTaskButton.interactable = true;

                Timer.text = "";

                return;
            }

            ulong diff = ((ulong)DateTime.Now.Ticks - lastOpen);
            ulong m = diff / TimeSpan.TicksPerMillisecond;
            float seconleft = (float)(msToWait - m) / 1000.0f;

            string t = "";

            t += ((int)seconleft / 3600).ToString() + "h:";
            seconleft -= ((int)seconleft / 3600) * 3600;
            t += ((int)seconleft / 60).ToString("00") + "m:";
            t += ((int)seconleft % 60).ToString("00") + "s";

            Timer.text = t;
        }
    }

    public void Click()
    {
        PlayerPrefs.SetInt("tasksRecordScore", 0);
        ProgressEveryDayTasks.points = 0;
        PlayerPrefs.SetInt("TasksJumps", 0);
        ProgressEveryDayTasks.jumps = 0;
        PlayerPrefs.SetInt("EveryDayTasksGamesPlayed", 0);
        ProgressEveryDayTasks.gamesPlayed = 0;
        PlayerPrefs.SetInt("EveryDayTasksFlyCoinsEarned", 0);
        ProgressEveryDayTasks.flyCoinsEarned = 0;

        Tasks[PlayerPrefs.GetInt("TaskEquiped")].SetActive(false);
        PlayerPrefs.SetInt("isReadyForTask", 1);

        lastOpen = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("lastOpen2", lastOpen.ToString());

        Done.SetActive(false);
        MainDone.SetActive(false);

        NewTaskButton.interactable = false;

        int count = PlayerPrefs.GetInt("EveryDayTasksDone");
        count++;
        PlayerPrefs.SetInt("EveryDayTasksDone", count);

        StartCoroutine("AnimationSeconds");
    }

    private bool isReady()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastOpen);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float seconleft = (float)(msToWait - m) / 1000.0f;

        if (seconleft < 0)
        {
            Timer.text = "";
            return true;
        }
        return false;
    }

    void ChangeTask()
    {
        index = r.Next(0, 9);
        PlayerPrefs.SetInt("TaskEquiped", index);
        PlayerPrefs.SetInt("isReadyForTask", 0);
    }
}
