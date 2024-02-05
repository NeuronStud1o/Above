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

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        equipedHero = PlayerPrefs.GetInt("currentHero");
    }
}
