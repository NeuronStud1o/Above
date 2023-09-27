using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    public static int coinsF;
    [SerializeField] private Text moneyText;

    [SerializeField] private AudioClip GetCoin;
    [SerializeField] private AudioSource coin;

    public static int coinsS;
    [SerializeField] private Text moneyText2;

    [SerializeField] private StartOnClick startOnClick;
    [SerializeField] private Buttons buttons;
    [SerializeField] private CoinSpawner coinSpawner;

    void Start()
    {
        startOnClick.player = GetComponent<Player>();
        buttons.Hero = gameObject;
        coinSpawner.Hero = gameObject;
        
        if (PlayerPrefs.HasKey("CoinsFAdd"))
        {
            PlayerPrefs.SetInt("CoinsFAdd", PlayerPrefs.GetInt("CoinsFAdd"));
        }
        else
        {
            PlayerPrefs.SetInt("CoinsFAdd", 1);
        }

        if (PlayerPrefs.HasKey("coinsF"))
        {
            coinsF = PlayerPrefs.GetInt("coinsF");
        }

        if (PlayerPrefs.HasKey("coinsS"))
        {
            coinsS = PlayerPrefs.GetInt("coinsS");
        }
        
        coin = GetComponentInChildren<AudioSource>();
        coin.volume = PlayerPrefs.GetFloat("Slider4");

        moneyText.text = coinsF + "";
        moneyText2.text = coinsS + "";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FlyCoin")
        {
            StartCoroutine(TouchCoin(collision.gameObject));
            coinsF += PlayerPrefs.GetInt("CoinsFAdd");
            PlayerPrefs.SetInt("coinsF", coinsF);
            coin.PlayOneShot(GetCoin);

            if (ProgressEveryDayTasks.flyCoinsEarned != 0)
            {
                int coins = PlayerPrefs.GetInt("EveryDayTasksFlyCoinsEarned");
                coins++;
                PlayerPrefs.SetInt("EveryDayTasksFlyCoinsEarned", coins);
            }

            moneyText.text = "" + coinsF;
        }

        if (collision.gameObject.tag == "SuperCoin")
        {
            StartCoroutine(TouchCoin(collision.gameObject));
            coinsS++;
            PlayerPrefs.SetInt("coinsS", coinsS);
            moneyText2.text = "" + coinsS;
            coin.PlayOneShot(GetCoin);
        }
    }

    IEnumerator TouchCoin(GameObject coin)
    {
        coin.GetComponentInChildren<Animator>().SetTrigger("Touch");
        yield return new WaitForSeconds(0.6f);
        Destroy(coin);
    }
}




