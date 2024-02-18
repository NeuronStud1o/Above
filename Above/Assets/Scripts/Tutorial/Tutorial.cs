using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [Header("#### FIRST PART")]
    [SerializeField] private GameObject FirstPart;

    [Space(10f)]
    [SerializeField] private Animator tutorialHeroAnimFirstPart;

    [Header("# UI")]
    [SerializeField] private GameObject touchScreenArrow;
    [SerializeField] private GameObject dialog;

    [Header("# DIALOG TEXTS")]
    [SerializeField] private GameObject thirdTextGO;

    [Header("#### SECOND PART")]
    [SerializeField] private GameObject SecondPart;

    [Space(10f)]
    [SerializeField] private Animator tutorialHeroAnimSecondPart;

    [Header("# UI")]
    [SerializeField] private GameObject touchScreenArrowSecondPart;
    [SerializeField] private GameObject jumpButtonSecondPart;
    [SerializeField] private GameObject jumpNoobButtonSecondPart;
    [SerializeField] private GameObject dialog2;
    [SerializeField] private GameObject timerGO;
    [SerializeField] private Animator upArrow;

    [Header("# DIALOG TEXTS")]
    [SerializeField] private GameObject fourthTextGO;

    [Header("#### THIRD PART")]
    [SerializeField] private GameObject ThirdPart;
    [SerializeField] private GameObject heroThirdPart;
    [SerializeField] private GameObject dialog3;
    [SerializeField] private GameObject jumpButtonThirdPart;

    [Header("#### ALL PARTS")]
    [SerializeField] private GameObject cameraGo;
    [SerializeField] private GameObject hero;
    [SerializeField] private GameObject finishPanel;
    private Rigidbody2D rb;

    private Animator camAnim;

    private int jumpCount = 0;
    private PlayerTutorial playerTutorial;

    void Start()
    {
        if (DataBase.instance != null)
        {
            DataBase.instance.GetComponent<AudioSource>().enabled = false;
        }
        
        rb = hero.GetComponent<Rigidbody2D>();
        camAnim = cameraGo.GetComponent<Animator>();
        playerTutorial = hero.GetComponent<PlayerTutorial>();

        StartCoroutine(FirstSequence());
    }

    public void FirstJump()
    {
        StartCoroutine(AnimationOfJump());
    }

    IEnumerator AnimationOfJump()
    {
        yield return new WaitForSeconds(2);

        tutorialHeroAnimFirstPart.SetTrigger("End");
        dialog.GetComponent<Animator>().SetTrigger("End");
        thirdTextGO.GetComponent<Animator>().SetTrigger("End");

        yield return new WaitForSeconds(1);

        camAnim.SetBool("FocusToHero", true);

        yield return new WaitForSeconds(2);

        touchScreenArrow.SetActive(true);
    }

    IEnumerator FirstSequence()
    {
        yield return new WaitForSeconds(1);

        FirstPart.SetActive(true);
    }

    public void Jump()
    {
        jumpCount++;

        if (jumpCount == 2)
        {
            StartCoroutine(EndFirstJump());
        }
    }

    private IEnumerator EndFirstJump()
    {
        camAnim.SetBool("FocusToHero", false);
        touchScreenArrow.SetActive(false);
        FirstPart.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        SecondPart.SetActive(true);
        fourthTextGO.GetComponent<Animator>().SetTrigger("End");

        yield return new WaitForSeconds(6.5f);

        tutorialHeroAnimSecondPart.SetTrigger("End");
        dialog2.GetComponent<Animator>().SetTrigger("End");

        yield return new WaitForSeconds(2f);

        timerGO.SetActive(true);

        playerTutorial.enabled = true;
        jumpButtonSecondPart.SetActive(true);
        camAnim.enabled = false;

        playerTutorial.Jump();

        JumpsPart();
    }

    public void JumpsPart()
    {
        touchScreenArrowSecondPart.SetActive(true);
        jumpButtonSecondPart.SetActive(false);
        jumpNoobButtonSecondPart.SetActive(true);
        
        SlowestSpeed();
    }

    private void SlowestSpeed()
    {
        RepeatingSlowestSpeed();
    }

    private void RepeatingSlowestSpeed()
    {
        if (playerTutorial.speed > 0.8f)
        {
            playerTutorial.speed -= 0.2f;
            Time.timeScale -= 0.1f;
            Invoke("SlowestSpeed", 0.1f);
        }
    }

    public void EndJumpsPart()
    {
        jumpButtonSecondPart.SetActive(true);
        jumpNoobButtonSecondPart.SetActive(false);
        touchScreenArrowSecondPart.SetActive(false);

        timerGO.GetComponent<Animator>().SetTrigger("End");
        upArrow.SetTrigger("End");

        FasterSpeed();
    }

    private void FasterSpeed()
    {
        RepeatingFasterSpeed();
    }

    private void RepeatingFasterSpeed()
    {
        if (playerTutorial.speed < 2.2f)
        {
            playerTutorial.speed += 0.2f;
            Time.timeScale += 0.1f;
            Invoke("FasterSpeed", 0.1f);
        }
    }

    public void OnEnemyTrigger()
    {
        jumpButtonThirdPart.SetActive(false);
        SecondPart.SetActive(false);

        camAnim.SetBool("Enemy", true);
        camAnim.enabled = true;

        ThirdPart.SetActive(true);

        rb.bodyType = RigidbodyType2D.Static;

        playerTutorial.enabled = false;
    }

    public void FamilizObstDone()
    {
        StartCoroutine(FamiliarizationWhithObstacle());
    }

    IEnumerator FamiliarizationWhithObstacle()
    {
        camAnim.SetBool("Enemy", false);

        yield return new WaitForSeconds(1f);

        camAnim.enabled = false;

        yield return new WaitForSeconds(2);

        jumpButtonThirdPart.SetActive(true);
        rb.bodyType = RigidbodyType2D.Dynamic;
        playerTutorial.enabled = true;

        yield return new WaitForSeconds(1);
        
        heroThirdPart.SetActive(false);
        dialog3.SetActive(false);

    }

    public void FinishTutorial()
    {
        Camera.main.GetComponent<AudioSource>().enabled = false;
        hero.SetActive(false);
        finishPanel.SetActive(true);
    }

    public void ReturnToLobby()
    {
        if (DataBase.instance != null)
        {
            DataBase.instance.GetComponent<AudioSource>().enabled = true;
        }

        SceneManager.LoadSceneAsync("MainMenu");
    }
}