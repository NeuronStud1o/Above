using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

enum Direction
{
    Right,
    Left
}

public class Player : MonoBehaviour
{
    private Direction direction;
    [SerializeField] private GameObject Panel;
    [SerializeField] private GameObject CanvasInGame;
    [SerializeField] private Score scoreScript;
    [SerializeField] private int coins;

    public float jumpForce = 7f;
    private Rigidbody2D rb;

    private AudioSource jumpSound;
    private Animator anim;
    [SerializeField] private Camera MainCamera;

    [SerializeField] private Color standartColor;

    [SerializeField] private AudioClip BrokenShield;

    private AudioSource cameraAudiosource;
    private CameraFollow cameraFollow;

    private int speedDirection = -1;

    private bool isCanMove = true;

    private void Awake()
    {
        jumpSound = GetComponent<AudioSource>();
        jumpSound.volume = PlayerPrefs.GetFloat("Slider4");
    }

    void Start()
    {
        cameraAudiosource = MainCamera.GetComponent<AudioSource>();
        cameraFollow = MainCamera.GetComponent<CameraFollow>();

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

        cameraFollow.doodlePos = transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
                    StartCoroutine(Death());
                }
                else if (PlayerPrefs.GetInt("HeroHP") == 1)
                {
                    GetComponent<AudioSource>().PlayOneShot(BrokenShield);
                    PlayerPrefs.SetInt("HeroHP", 0);

                    GetComponent<SpriteRenderer>().color = standartColor;

                    if (direction == Direction.Right)
                    {
                        TakeDirection(Direction.Left);
                    }
                    else if (direction == Direction.Left)
                    {
                        TakeDirection(Direction.Right);;
                    }
                }
            
            }

            if (collision.collider.CompareTag("DownEnemy"))
            {
                StartCoroutine(Death());
            }
        }
        
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
            transform.position += Vector3.left * PlayerPrefs.GetFloat("Speed") * Time.deltaTime * speedDirection;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
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
    
    IEnumerator Death()
    {
        CanvasInGame.SetActive(false);
        Handheld.Vibrate();
        cameraFollow.enabled = false;
        cameraAudiosource.enabled = false;
        isCanMove = false;
        GetComponent<Collider2D>().enabled = false;
        
        int lastRunScore = int.Parse(scoreScript.scoreText.text.ToString());
        PlayerPrefs.SetInt("lastRunScore", lastRunScore);

        yield return new WaitForSeconds(0.05f);

        MainCamera.gameObject.transform.position = new Vector3(-0.05f, MainCamera.gameObject.transform.position.y, MainCamera.gameObject.transform.position.z);

        yield return new WaitForSeconds(0.05f);

        MainCamera.gameObject.transform.position = new Vector3(0.05f, MainCamera.gameObject.transform.position.y, MainCamera.gameObject.transform.position.z);

        yield return new WaitForSeconds(0.05f);

        MainCamera.gameObject.transform.position = new Vector3(-0.05f, MainCamera.gameObject.transform.position.y, MainCamera.gameObject.transform.position.z);

        yield return new WaitForSeconds(0.05f);

        MainCamera.gameObject.transform.position = new Vector3(0.05f, MainCamera.gameObject.transform.position.y, MainCamera.gameObject.transform.position.z);

        yield return new WaitForSeconds(0.05f);

        MainCamera.gameObject.transform.position = new Vector3(-0.05f, MainCamera.gameObject.transform.position.y, MainCamera.gameObject.transform.position.z);

        yield return new WaitForSeconds(0.05f);

        MainCamera.gameObject.transform.position = new Vector3(0.05f, MainCamera.gameObject.transform.position.y, MainCamera.gameObject.transform.position.z);

        yield return new WaitForSeconds(0.05f);

        MainCamera.gameObject.transform.position = new Vector3(-0.05f, MainCamera.gameObject.transform.position.y, MainCamera.gameObject.transform.position.z);

        yield return new WaitForSeconds(0.05f);

        MainCamera.gameObject.transform.position = new Vector3(0.05f, MainCamera.gameObject.transform.position.y, MainCamera.gameObject.transform.position.z);

        yield return new WaitForSeconds(0.05f);

        MainCamera.gameObject.transform.position = new Vector3(-0.05f, MainCamera.gameObject.transform.position.y, MainCamera.gameObject.transform.position.z);

        yield return new WaitForSeconds(0.05f);

        MainCamera.gameObject.transform.position = new Vector3(-0.05f, MainCamera.gameObject.transform.position.y, MainCamera.gameObject.transform.position.z);

        yield return new WaitForSeconds(0.05f);

        MainCamera.gameObject.transform.position = new Vector3(0f, MainCamera.gameObject.transform.position.y, MainCamera.gameObject.transform.position.z);

        yield return new WaitForSeconds(0.45f);
        
        Panel.SetActive(true);
    }
}
