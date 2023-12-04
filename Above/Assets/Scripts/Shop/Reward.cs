using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Reward : MonoBehaviour
{
    [SerializeField] private float msToWait = 86400000f;
    private int notifIndex = 0;
    private bool isReadyOneCheck = true;
    private Text Timer;

    private ulong lastOpen;

    public GameObject Done;
    public GameObject MainDone;

    public GameObject Coin;
    public GameObject coinWindow;

    [SerializeField] private CoinsManagerInMainMenu coinsManagerInMainMenu;
    [SerializeField] private Button RewardButton;
    [SerializeField] private NotificationsManager notifManager;

    void Start()
    {
        if (!PlayerPrefs.HasKey("lastOpen"))
        {
            PlayerPrefs.SetString("lastOpen", "0");
        }

        lastOpen = ulong.Parse(PlayerPrefs.GetString("lastOpen"));
        Timer = RewardButton.GetComponentInChildren<Text>();

        if (!isReady())
        {
            Done.SetActive(false);
            MainDone.SetActive(false);
            RewardButton.interactable = false;
        }
    }

    IEnumerator AnimationSeconds()
    {
        yield return new WaitForSeconds(2);

        coinWindow.SetActive(false);

        int allCoins = PlayerPrefs.GetInt("coinsF");
        allCoins += 10;
        PlayerPrefs.SetInt("coinsF", allCoins);

        coinsManagerInMainMenu.UpdateUI();
    }
  
    void Update()
    {
        if (isReady() && isReadyOneCheck)
        {
            Done.SetActive(true);
            MainDone.SetActive(true);
            Coin.SetActive(true);
            Timer.text = "10   ";

            RewardButton.interactable = true;
            int count = 0;

            foreach (int i in notifManager.iconsIndex)
            {
                if (i == 0)
                {
                    notifIndex = notifManager.iconsIndex[count];
                    notifManager.iconsIndex[count] = 1;

                    isReadyOneCheck = false;

                    notifManager.SetValue();

                    return;
                }
                count++;
            }

            notifManager.SetValue();
            isReadyOneCheck = false;

            return;
        }

        if (!RewardButton.IsInteractable())
        {
            Coin.SetActive(false);
            
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
        lastOpen = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("lastOpen", lastOpen.ToString());

        Done.SetActive(false);
        MainDone.SetActive(false);

        Coin.SetActive(false);
        RewardButton.interactable = false;

        int count = PlayerPrefs.GetInt("RewardsCount");
        count++;
        PlayerPrefs.SetInt("RewardsCount", count);
            
        coinWindow.SetActive(true);
        StartCoroutine(AnimationSeconds());

        notifManager.RemoveIconWhithList(notifIndex);
        isReadyOneCheck = true;
    }

    public bool isReady()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastOpen);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float seconleft = (float)(msToWait - m) / 1000.0f;

        if (seconleft < 0)
        {
            PlayerPrefs.SetString("lastOpen", "0");

            return true;
        }

        return false;
    }
}