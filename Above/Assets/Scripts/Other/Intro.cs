using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject videoPlayer;

    void Start()
    {
        StartCoroutine(IntroAsync());
    }

    private IEnumerator IntroAsync()
    {
        yield return new WaitForSeconds(1f);
        
        videoPlayer.SetActive(true);

        yield return new WaitForSeconds(7);

        SceneManager.LoadSceneAsync("Authentication");
    }
}
