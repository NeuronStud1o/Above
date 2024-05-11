using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerOffline : MonoBehaviour
{
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip getCoin;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject canvasInGame;

    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private Animator anim;
    private CameraFollowOffline cameraFollow;
    private int speedDirection = -1;
    
    private float speed = 2.2f;

    public bool isCanMove = false;
    bool isCanTouchCoin = true;

    private System.Random random = new System.Random();

    void Start()
    {
        DataBase.instance.SetActiveLoadingScreen(false);
        
        StartOnClick.instance.playerOffline = this;

        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cameraFollow = Camera.main.GetComponent<CameraFollowOffline>();

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

            if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("DownEnemy"))
            {
                await Death();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FlyCoin" && isCanTouchCoin)
        {
            PlayerPrefs.SetInt("coinsF", PlayerPrefs.GetInt("coinsF") + 1);
            Debug.Log(2);
            StartCoroutine(TouchCoin(collision.gameObject));
            audioSource.PlayOneShot(getCoin);

            OfflineGameManager.instance.UpdateUI();
        }

        if (collision.gameObject.tag == "SuperCoin" && isCanTouchCoin)
        {
            PlayerPrefs.SetInt("coinsS", PlayerPrefs.GetInt("coinsS") + 1);
            StartCoroutine(TouchCoin(collision.gameObject));
            audioSource.PlayOneShot(getCoin);

            OfflineGameManager.instance.UpdateUI();
        }
    }

    IEnumerator TouchCoin(GameObject coin)
    {
        isCanTouchCoin = false;
        coin.GetComponentInChildren<Animator>().SetTrigger("Touch");

        yield return new WaitForSeconds(0.6f);

        isCanTouchCoin = true;
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
        Handheld.Vibrate();
        
        audioSource.PlayOneShot(death);
        canvasInGame.SetActive(false);
        
        cameraFollow.enabled = false;
        Camera.main.GetComponent<AudioSource>().enabled = false;
        isCanMove = false;
        
        GetComponent<Collider2D>().enabled = false;

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

        AdsManager.instance.AdvertisingProcessor();

        deathPanel.SetActive(true);
    }
}
