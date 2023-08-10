using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Direction
{
    Right,
    Left
}

public class Player : MonoBehaviour
{
    private Direction direction;
    public GameObject Panel;
    [SerializeField] private Score scoreScript;
    [SerializeField] private int coins;

    public float jumpForce = 7f;
    Rigidbody2D rb;

    AudioSource jumpSound;
    private Animator anim;
    public Camera MainCamera;

    public Color standartColor;

    public AudioClip BrokenShield;

    private void Awake()
    {
        jumpSound = GetComponent<AudioSource>();
        jumpSound.volume = PlayerPrefs.GetFloat("Slider4");
    }

    void Start()
    {
        Buttons.Hero = gameObject;

        if (PlayerPrefs.HasKey("Speed"))
        {
            PlayerPrefs.SetFloat("Speed", PlayerPrefs.GetFloat("Speed"));
        }
        else
        {
            PlayerPrefs.SetFloat("Speed", 2.2f);
        }

        direction = Direction.Right;
        rb = GetComponent<Rigidbody2D>();

        
        anim = GetComponent<Animator>();

        MainCamera.GetComponent<CameraFollow>().doodlePos = transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("RightWall"))
        {
            direction = Direction.Left;

        }
        if (collision.collider.CompareTag("LeftWall"))
        {
            direction = Direction.Right;
        }

        if (collision.collider.CompareTag("Enemy"))
        {
            if (PlayerPrefs.GetInt("HeroHP") == 0)
            {
                GetComponent<Player>().enabled = false;
                Panel.SetActive(true);
                int lastRunScore = int.Parse(scoreScript.scoreText.text.ToString());
                PlayerPrefs.SetInt("lastRunScore", lastRunScore);
                MainCamera.GetComponent<AudioSource>().enabled = false;
            }
            else if (PlayerPrefs.GetInt("HeroHP") == 1)
            {
                GetComponent<AudioSource>().PlayOneShot(BrokenShield);
                PlayerPrefs.SetInt("HeroHP", 0);

                GetComponent<SpriteRenderer>().color = standartColor;

                if (direction == Direction.Right)
                {
                    direction = Direction.Left;
                }
                else if (direction == Direction.Left)
                {
                    direction = Direction.Right;
                }
            }
        }
    }

    void Update()
    {      
            switch (direction)
            {
                case Direction.Left:
                    transform.localScale = new Vector3(-0.2954769f, 0.2954769f, 0f);
                    transform.position += Vector3.left * PlayerPrefs.GetFloat("Speed") * Time.deltaTime;
                    break;
                case Direction.Right:
                    transform.localScale = new Vector3(0.2954769f, 0.2954769f, 0f);
                    transform.position += Vector3.right * PlayerPrefs.GetFloat("Speed") * Time.deltaTime;
                    break;
            }

            rb.velocity = new Vector2(0, rb.velocity.y);

        if (GetComponent<Player>().enabled == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                rb.velocity = new Vector2(0, jumpForce);
                jumpSound.Play();
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
        }
    }
}
