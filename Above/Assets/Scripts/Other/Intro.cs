using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject videoPlayer;

    void Start()
    {
        StartCoroutine(IntroAsync());
    }

    private IEnumerator IntroAsync()
    {
        float duration = (float)videoPlayer.GetComponent<VideoPlayer>().clip.length + 1;
        Debug.Log("" + duration);

        yield return new WaitForSeconds(1f);
        
        videoPlayer.SetActive(true);

        yield return new WaitForSeconds(duration);

        SceneManager.LoadSceneAsync("Authentication");
    }
}
