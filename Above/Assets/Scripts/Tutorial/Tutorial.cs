using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [Header("#### FIRST PART")]
    [SerializeField] private GameObject FirstPart;
    [Header("# UI")]
    [SerializeField] private GameObject touchScreenArrow;
    [SerializeField] private GameObject heroFirstPart;
    [SerializeField] private GameObject dialog;
    [Header("# DIALOG TEXTS")]
    [SerializeField] private GameObject thirdTextGO;

    [Header("#### SECOND PART")]
    [SerializeField] private GameObject SecondPart;
    [Header("# UI")]
    [SerializeField] private GameObject touchScreenArrowSecondPart;
    [SerializeField] private GameObject jumpButtonSecondPart;
    [SerializeField] private GameObject jumpNoobButtonSecondPart;
    [Header("# DIALOG TEXTS")]
    [SerializeField] private GameObject fourthTextGO;
    [SerializeField] private GameObject fivethTextGO;

    [Header("#### ALL PARTS")]
    [SerializeField] private GameObject cameraGo;
    [SerializeField] private GameObject hero;
    private Rigidbody2D rb;

    private Animator camAnim;
    private Animator tutorialHeroAnimFirstPart;

    private int jumpCount = 0;

    private PlayerTutorial playerTutorial;

    void Start()
    {
        rb = hero.GetComponent<Rigidbody2D>();
        camAnim = cameraGo.GetComponent<Animator>();
        tutorialHeroAnimFirstPart = heroFirstPart.GetComponent<Animator>();
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

        yield return new WaitForSeconds(5);

        playerTutorial.enabled = true;
        jumpButtonSecondPart.SetActive(true);
        camAnim.enabled = false;
    }

    public void IfNotJump()
    {
        fourthTextGO.SetActive(false);
        fivethTextGO.SetActive(true);
        SlowestSpeed();
        touchScreenArrowSecondPart.SetActive(true);
        jumpButtonSecondPart.SetActive(false);
        jumpNoobButtonSecondPart.SetActive(true);
    }

    private void SlowestSpeed()
    {
        RepeatingSlowestSpeed();
    }

    private void RepeatingSlowestSpeed()
    {
        if (playerTutorial.speed > 0.8f)
        {
            print(Time.timeScale);
            print(playerTutorial.speed);
            playerTutorial.speed -= 0.2f;
            Time.timeScale -= 0.1f;
            Invoke("SlowestSpeed", 0.1f);
        }
    }

    public void EndIfNotJump()
    {
        jumpButtonSecondPart.SetActive(true);
        jumpNoobButtonSecondPart.SetActive(false);
        touchScreenArrowSecondPart.SetActive(false);
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
            print(Time.timeScale);
            print(playerTutorial.speed);
            playerTutorial.speed += 0.2f;
            Time.timeScale += 0.1f;
            Invoke("FasterSpeed", 0.1f);
        }
    }
}