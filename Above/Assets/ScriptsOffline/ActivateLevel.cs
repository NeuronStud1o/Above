using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLevel : MonoBehaviour
{
    [SerializeField] private GameObject blockPanel;
    [SerializeField] private int numOfLevel;

    void Start()
    {
        if (PlayerPrefs.HasKey("Levels"))
        {
            if (numOfLevel <= PlayerPrefs.GetInt("Levels"))
            {
                blockPanel.SetActive(false);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Levels", 1);
        }
    }
}
