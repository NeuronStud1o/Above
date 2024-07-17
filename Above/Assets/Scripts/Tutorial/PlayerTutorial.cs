using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorial : MonoBehaviour
{
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private Animator anim;
    [SerializeField] Tutorial tutorial;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject afterDeathButton;
    [SerializeField] private GameObject deathText;

    public float jumpForce = 5f;
    public float speed = 2.2f;

    private CameraFollow cameraFollow;

    private int speedDirection = -1;

    private bool isCanMove = true;
    int jumpCount = 0;
    bool canChangeRotation = true;

    void Start()
    {
        cameraFollow = MainCamera.GetComponent<CameraFollow>();

        cameraFollow.doodlePos = transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isCanMove)
        {
            if (collision.collider.CompareTag("RightWall") && canChangeRotation)
            {
                TakeDirection();
            }
            if (collision.collider.CompareTag("LeftWall") && canChangeRotation)
            {
                TakeDirection();
            }

            if (collision.collider.CompareTag("Enemy"))
            {
                transform.position = respawnPoint.position;
                MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, respawnPoint.position.y, MainCamera.transform.position.z);
                rb.bodyType = RigidbodyType2D.Static;
                afterDeathButton.SetActive(true);
                deathText.SetActive(true);
                enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "TutorialEnemyTrigger")
        {
            tutorial.OnEnemyTrigger();
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "FinishTutorial")
        {
            tutorial.FinishTutorial();
            other.gameObject.SetActive(false);
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

        jumpSound.PlayOneShot(jumpClip);
        
        anim.SetTrigger("Jump");
    }

    public void NoobJump()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;

        if (jumpCount < 4)
        {
            jumpCount++;
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.velocity = new Vector2(0, jumpForce);

            jumpSound.PlayOneShot(jumpClip);
            
            anim.SetTrigger("Jump");
        }
        else
        {
            tutorial.EndJumpsPart();
        }
    }

    public void AfterDeath()
    {
        deathText.SetActive(false);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
