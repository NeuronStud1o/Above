using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int eqipedLevel = 1;
    public int equipedHero = 0;

    void Awake()
    {
        //PlayerPrefs.SetInt("Levels", 1);

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        equipedHero = PlayerPrefs.GetInt("currentHero");

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        DataBase.instance.GetComponent<AudioSource>().enabled = true;
    }
}
