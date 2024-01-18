using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using TMPro;

public class Reward : MonoBehaviour
{
    [SerializeField] private float msToWait = 86400000f;
    [SerializeField] private Button RewardButton;

    private TextMeshProUGUI Timer;
    private ulong lastOpen;

    public GameObject Done;
    public GameObject MainDone;

    public GameObject Coin;
    public GameObject coinWindow;


    void Start()
    {
        if (!PlayerPrefs.HasKey("lastOpen"))
        {
            PlayerPrefs.SetString("lastOpen", "0");
        }

        lastOpen = ulong.Parse(PlayerPrefs.GetString("lastOpen"));
        Timer = RewardButton.GetComponentInChildren<TextMeshProUGUI>();

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

        CoinsManagerInMainMenu.instance.coinsF += 10;
        CoinsManagerInMainMenu.instance.UpdateUI();

        JsonStorage.instance.jsonData.userData.coinsF = CoinsManagerInMainMenu.instance.coinsF;
        JsonStorage.instance.jsonData.userData.coinsFAllTime += 10;

        JsonStorage.instance.SaveData();
    }
  
    void Update()
    {
        if(!RewardButton.IsInteractable())
        {
            if(isReady())
            {
                Done.SetActive(true);
                MainDone.SetActive(true);
                Coin.SetActive(true);

                RewardButton.interactable = true;

                Timer.text = "10  .";

                return;
            }
            else
            {
                Coin.SetActive(false);
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
        lastOpen = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("lastOpen", lastOpen.ToString());

        Done.SetActive(false);
        MainDone.SetActive(false);

        Coin.SetActive(false);
        RewardButton.interactable = false;

        coinWindow.SetActive(true);
        StartCoroutine(AnimationSeconds());
    }

    public bool isReady()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastOpen);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float seconleft = (float)(msToWait - m) / 1000.0f;

        if (seconleft < 0)
        {
            return true;
        }

        return false;
    }
}
