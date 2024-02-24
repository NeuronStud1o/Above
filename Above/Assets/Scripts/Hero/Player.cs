using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

enum Direction
{
    Right,
    Left
}

public class Player : MonoBehaviour
{
    /*[SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip brokenShield;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip getCoin;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject canvasInGame;
    [SerializeField] private Color standartColor;

    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private Animator anim;
    private CameraFollow cameraFollow;
    private int speedDirection = -1;
    
    private float speed = 0;
    private int hp = 0;
    private int coinsToAdd = 1;

    public bool isCanMove = false;
    private System.Random random = new System.Random();

    void Start()
    {
        StartOnClick.instance.player = this;
        CoinSpawner.instance.hero = gameObject;

        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        audioSource.volume = JsonStorage.instance.jsonData.audioSettings.sfxGame;
        
        hp = JsonStorage.instance.jsonData.currentShop.currentBoost == 3 ? 1 : 0;
        speed = JsonStorage.instance.jsonData.currentShop.currentBoost == 2 ? 1.5f : 2.2f;
        coinsToAdd = JsonStorage.instance.jsonData.currentShop.currentBoost == 1 ? 2 : 1;

        PauseController.instance.Hero = gameObject;

        cameraFollow.doodlePos = transform;
    }

    private async void OnCollisionEnter2D(Collision2D collision)
    {
        if (isCanMove)
        {
            if (collision.collider.CompareTag("RightWall"))
            {
                TakeDirection(Direction.Left);
            }
            if (collision.collider.CompareTag("LeftWall"))
            {
                TakeDirection(Direction.Right);
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
        if (collision.gameObject.tag == "FlyCoin")
        {
            StartCoroutine(TouchCoin(collision.gameObject));

            CoinsManager.instance.coinsF += coinsToAdd;
            JsonStorage.instance.jsonData.userData.coinsF = CoinsManager.instance.coinsF;

            JsonStorage.instance.jsonData.userData.coinsFAllTime += coinsToAdd;

            audioSource.PlayOneShot(getCoin);

            CoinsManager.instance.UpdateUI();
        }

        if (collision.gameObject.tag == "SuperCoin")
        {
            StartCoroutine(TouchCoin(collision.gameObject));

            CoinsManager.instance.coinsS++;
            JsonStorage.instance.jsonData.userData.coinsS = CoinsManager.instance.coinsS;

            JsonStorage.instance.jsonData.userData.coinsSAllTime++;
            
            audioSource.PlayOneShot(getCoin);

            CoinsManager.instance.UpdateUI();
        }
    }

    IEnumerator TouchCoin(GameObject coin)
    {
        coin.GetComponentInChildren<Animator>().SetTrigger("Touch");

        yield return new WaitForSeconds(0.6f);

        Destroy(coin);
    }

    void TakeDirection(Direction flip)
    {
        switch (flip)
        {
            case Direction.Left:
                transform.localScale = new Vector3(-0.2954769f, 0.2954769f, 0f);
                speedDirection = 1;
                break;
            case Direction.Right:
                transform.localScale = new Vector3(0.2954769f, 0.2954769f, 0f);
                speedDirection = -1;
                break;
        }
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
        
        if (JsonStorage.instance.jsonData.otherSettings.vibration)
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

        if (JsonStorage.instance.jsonData.otherSettings.cameraShake)
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


        await Task.Delay(TimeSpan.FromSeconds(0.45f));

        int a = random.Next(1, 5);

        if (a == 2)
        {
            InterstitialAds.instance.Show();
        }

        deathPanel.SetActive(true);
        LosePanel.instance.Death(int.Parse(Score.instance.scoreText.text));
    }*/
}