using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    public static int coinsF;
    public Text moneyText;
    public GameObject CoinsF;

    public AudioClip GetCoin;
    public AudioSource coin;

    public static int coinsS;
    public Text moneyText2;
    public GameObject CoinsS;

    void Start()
    {
        Buttons.Hero = gameObject;
        CoinSpawner.Hero = gameObject;
        
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
            coinsF = PlayerPrefs.GetInt("coinsF",coinsF);
        }

        if (PlayerPrefs.HasKey("coinsS"))
        {
            coinsS = PlayerPrefs.GetInt("coinsS");
        }
        
        coin = GetComponentInChildren<AudioSource>();
        coin.volume = PlayerPrefs.GetFloat("Slider4");
    }
    void FixedUpdate()
    {
        PlayerPrefs.SetInt("coinsF", coinsF);
        coinsF = PlayerPrefs.GetInt("coinsF");
        moneyText.text = "" + coinsF;

        PlayerPrefs.SetInt("coinsS", coinsS);
        coinsS = PlayerPrefs.GetInt("coinsS");
        moneyText2.text = "" + coinsS;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FlyCoin")
        {
            collision.gameObject.tag = "Untagged";
            StartCoroutine(TouchCoin(collision.gameObject));
            coinsF += PlayerPrefs.GetInt("CoinsFAdd");
            coin.PlayOneShot(GetCoin);

            if (ProgressEveryDayTasks.flyCoinsEarned != 0)
            {
                int coins = PlayerPrefs.GetInt("EveryDayTasksFlyCoinsEarned");
                coins++;
                PlayerPrefs.SetInt("EveryDayTasksFlyCoinsEarned", coins);
            }
        }

        if (collision.gameObject.tag == "SuperCoin")
        {
            collision.gameObject.tag = "Untagged";
            StartCoroutine(TouchCoin(collision.gameObject));
            coinsS++;
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




