using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject timerGameObject;
    private GameObject hero;
    private GameObject timerChildren;
    public GameObject Hero { get => hero; set => hero = value; }
    public static PauseController instance;

    public bool isPause = false;

    void Start()
    {
        instance = this;
        timerChildren = timerGameObject.GetComponentInChildren<Canvas>().gameObject;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        if (hero != null)
        {
            hero.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            hero.GetComponent<Player>().enabled = false;
            isPause = true;
        }
    }

    public void ContinueGame()
    {
        timerGameObject.SetActive(true);
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        Time.timeScale = 1;

        yield return null;

        while (timerChildren.activeSelf)
        {
            yield return null;
        }

        timerGameObject.SetActive(false);
        hero.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        if (hero != null)
        {
            Player playerComponent = hero.GetComponent<Player>();
            if (playerComponent != null)
            {
                playerComponent.enabled = true;
            }
        }

        isPause = false;
    }
}
