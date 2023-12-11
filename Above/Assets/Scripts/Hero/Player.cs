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
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip brokenShield;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip getCoin;
    [SerializeField] private GameObject Panel;
    [SerializeField] private GameObject canvasInGame;
    [SerializeField] private Score scoreScript;
    [SerializeField] private Color standartColor;

    public float jumpForce = 7f;
    private Direction direction;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private Animator anim;
    private CameraFollow cameraFollow;
    private int speedDirection = -1;
    private bool isCanMove = true;

    async void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        audioSource.volume = PlayerPrefs.GetFloat("Slider4");
        audioSource.volume = await DataBase.instance.LoadDataFloat("menu", "settings", "audio", "sfxGameSA");
        
        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        if (await DataBase.instance.LoadDataCheck("player", "speed") == false)
        {
            DataBase.instance.SaveData(2.2f, "player", "speed");
        }

        direction = Direction.Right;
        rb = GetComponent<Rigidbody2D>();
        
        anim = GetComponent<Animator>();
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
                if (PlayerPrefs.GetInt("HeroHP") == 0)
                {
                    await Death();
                }
                else if (PlayerPrefs.GetInt("HeroHP") == 1)
                {
                    GetComponent<AudioSource>().PlayOneShot(brokenShield);
                    PlayerPrefs.SetInt("HeroHP", 0);

                    GetComponent<SpriteRenderer>().color = standartColor;

                    if (direction == Direction.Right)
                    {
                        TakeDirection(Direction.Right);
                    }
                    else if (direction == Direction.Left)
                    {
                        TakeDirection(Direction.Left);;
                    }
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
            CoinsManager.instance.coinsF += PlayerPrefs.GetInt("CoinsFAdd");
            //PlayerPrefs.SetInt("coinsF", coinsF);
            audioSource.PlayOneShot(getCoin);

            if (ProgressEveryDayTasks.flyCoinsEarned != 0)
            {
                int coins = PlayerPrefs.GetInt("EveryDayTasksFlyCoinsEarned");
                coins++;
                PlayerPrefs.SetInt("EveryDayTasksFlyCoinsEarned", coins);
            }

            CoinsManager.instance.UpdateUI();
        }

        if (collision.gameObject.tag == "SuperCoin")
        {
            StartCoroutine(TouchCoin(collision.gameObject));
            CoinsManager.instance.coinsS++;
            //PlayerPrefs.SetInt("coinsS", coinsS);
            
            CoinsManager.instance.UpdateUI();

            audioSource.PlayOneShot(getCoin);
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
                direction = Direction.Right;
                break;
            case Direction.Right:
                transform.localScale = new Vector3(0.2954769f, 0.2954769f, 0f);
                speedDirection = -1;
                direction = Direction.Left;
                break;
        }
    }

    void Update()
    {   
        if (isCanMove)
        {
            transform.position += Vector3.left * PlayerPrefs.GetFloat("Speed") * Time.deltaTime * speedDirection;
        } 
    }

    public void Jump()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.velocity = new Vector2(0, jumpForce);

        audioSource.PlayOneShot(jumpClip);
        
        anim.SetTrigger("Jump");

        int taskJump = PlayerPrefs.GetInt("Jumps");
        taskJump++;
        PlayerPrefs.SetInt("Jumps", taskJump);

        if (ProgressEveryDayTasks.jumps != 0)
        {
            int jumps = PlayerPrefs.GetInt("TasksJumps");
            jumps++;
            PlayerPrefs.SetInt("TasksJumps", jumps);
        }
    }
    
    private async Task Death()
    {
        Handheld.Vibrate();
        audioSource.PlayOneShot(death);
        canvasInGame.SetActive(false);
        
        cameraFollow.enabled = false;
        Camera.main.GetComponent<AudioSource>().enabled = false;
        isCanMove = false;
        
        GetComponent<Collider2D>().enabled = false;

        int lastRunScore = int.Parse(scoreScript.scoreText.text);
        PlayerPrefs.SetInt("lastRunScore", lastRunScore);

        await Task.Delay(TimeSpan.FromSeconds(0.05f));

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

        await Task.Delay(TimeSpan.FromSeconds(0.45f));

        Panel.SetActive(true);
    }
}