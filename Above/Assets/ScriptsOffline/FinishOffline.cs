using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishOffline : MonoBehaviour
{
    [SerializeField] private GameObject finishPanel;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (PlayerPrefs.GetInt("Levels") < LevelManager.instance.eqipedLevel)
            {
                PlayerPrefs.SetInt("Levels", LevelManager.instance.eqipedLevel);
            }

            finishPanel.SetActive(true);
            Camera.main.GetComponent<AudioSource>().enabled = false;
            other.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            other.gameObject.GetComponent<PlayerOffline>().isCanMove = false;
        }
    }
}
