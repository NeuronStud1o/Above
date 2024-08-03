using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip brokenShield;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip getCoin;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject canvasInGame;
    [SerializeField] private Color standartColor;
    [SerializeField] private AudioSource audioSource;

    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private Animator anim;
    private CameraFollow cameraFollow;
    [SerializeField] private int speedDirection = -1;
    
    private float speed = 0;
    private int hp = 0;
    private int coinsToAdd = 1;

    public bool isCanMove = false;

    bool canTouchFlycoin = true;
    bool canTouchSupercoin = true;
    bool canChangeRotation = true;

    void Start()
    {
        StartOnClick.instance.player = this;
        CoinSpawner.instance.hero = gameObject;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        audioSource.volume = JsonStorage.instance.data.audioSettings.sfxGame;
        
        hp = JsonStorage.instance.data.currentShop.currentBoost == 3 ? 1 : 0;
        speed = JsonStorage.instance.data.currentShop.currentBoost == 2 ? 1.5f : 2.2f;
        coinsToAdd = JsonStorage.instance.data.currentShop.currentBoost == 1 ? 2 : 1;

        PauseController.instance.Hero = gameObject;

        cameraFollow.doodlePos = transform;
    }

    private async void OnCollisionEnter2D(Collision2D collision)
    {
        if (isCanMove)
        {
            if (collision.collider.CompareTag("RightWall") && canChangeRotation)
            {
                canChangeRotation = false;
                TakeDirection();
            }
            if (collision.collider.CompareTag("LeftWall") && canChangeRotation)
            {
                canChangeRotation = false;
                TakeDirection();
            }

            if (collision.collider.CompareTag("Enemy"))
            {
                if (hp == 0)
                {
                    await Death();
                }
                else if (hp == 1)
                {
                    audioSource.PlayOneShot(brokenShield);
                    hp = 0;

                    GetComponent<SpriteRenderer>().color = standartColor;
                }
            }

            if (collision.collider.CompareTag("DownEnemy"))
            {
                await Death();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FlyCoin" && canTouchFlycoin)
        {
            StartCoroutine(TouchCoin(collision.gameObject, true));

            CoinsManager.instance.coinsF += coinsToAdd;
            JsonStorage.instance.data.userData.coinsF = CoinsManager.instance.coinsF;

            JsonStorage.instance.data.userData.coinsFAllTime += coinsToAdd;

            CryptoHelper.Encrypt(JsonStorage.instance.data, JsonStorage.instance.password);

            audioSource.PlayOneShot(getCoin);

            CoinsManager.instance.UpdateUI();
        }

        if (collision.gameObject.tag == "SuperCoin" && canTouchSupercoin)
        {
            StartCoroutine(TouchCoin(collision.gameObject, false));

            CoinsManager.instance.coinsS++;
            JsonStorage.instance.data.userData.coinsS = CoinsManager.instance.coinsS;

            JsonStorage.instance.data.userData.coinsSAllTime++;

            CryptoHelper.Encrypt(JsonStorage.instance.data, JsonStorage.instance.password);
            
            audioSource.PlayOneShot(getCoin);

            CoinsManager.instance.UpdateUI();
        }
    }

    IEnumerator TouchCoin(GameObject coin, bool isFlycoin)
    {
        if (isFlycoin)
        {
            canTouchFlycoin = false;
        }
        else
        {
            canTouchSupercoin = false;
        }

        coin.GetComponentInChildren<Animator>().SetTrigger("Touch");

        yield return new WaitForSeconds(0.6f);

        Destroy(coin);

        if (isFlycoin)
        {
            canTouchFlycoin = true;
        }
        else
        {
            canTouchSupercoin = true;
        }
    }

    void TakeDirection()
    {
        StartCoroutine(NewDirection());
    }

    IEnumerator NewDirection()
    {
        anim.SetBool("RightDirection", !anim.GetBool("RightDirection"));
        speedDirection *= -1;

        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        yield return new WaitForSeconds(0.5f);

        canChangeRotation = true;
    }

    void Update()
    {   
        if (isCanMove)
        {
            transform.position += Vector3.left * speed * Time.deltaTime * speedDirection;
        } 
    }

    public void Jump()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.velocity = new Vector2(0, jumpForce);

        audioSource.PlayOneShot(jumpClip);
        
        anim.SetTrigger("Jump");
    }
    
    private async Task Death()
    {
        canvasInGame.SetActive(false);
        
        if (JsonStorage.instance.data.otherSettings.vibration)
        {
            Handheld.Vibrate();
            Debug.Log("Vibration");
        }
        
        audioSource.PlayOneShot(death);
        
        cameraFollow.enabled = false;
        Camera.main.GetComponent<AudioSource>().enabled = false;
        isCanMove = false;
        
        GetComponent<Collider2D>().enabled = false;

        await Task.Delay(TimeSpan.FromSeconds(0.05f));

        if (JsonStorage.instance.data.otherSettings.cameraShake)
        {
            for (int i = 0; i < 10; i++)
            {
                if (i % 2 != 0)
                {
                    await Task.Delay(TimeSpan.FromSeconds(0.05f));
                    Camera.main.gameObject.transform.position = new Vector3(-0.1f, Camera.main.gameObject.transform.position.y, Camera.main.gameObject.transform.position.z);
                }
                else
                {
                    await Task.Delay(TimeSpan.FromSeconds(0.05f));
                    Camera.main.gameObject.transform.position = new Vector3(0.1f, Camera.main.gameObject.transform.position.y, Camera.main.gameObject.transform.position.z);
                }
            }
        }

        Camera.main.gameObject.transform.position = new Vector3(0, Camera.main.gameObject.transform.position.y, Camera.main.gameObject.transform.position.z);

        await Task.Delay(TimeSpan.FromSeconds(0.45f));

        AdsManager.instance.AdvertisingProcessor();

        deathPanel.SetActive(true);
        LosePanel.instance.Death(int.Parse(Score.instance.scoreText.text));
    }

    
}