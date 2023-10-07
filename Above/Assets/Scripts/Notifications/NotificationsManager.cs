using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class NotificationsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] icons;
    [SerializeField] private List<GameObject> panelIcons;
    public int[] iconsIndex = new int[3];

    void Start()
    {
        if (PlayerPrefs.GetInt("FirstTimeInGameForNotifications") == 0)
        {
            SaveGame();
            PlayerPrefs.SetInt("FirstTimeInGameForNotifications", 1);
        }

        ReadFile();

        for (int i = 0; i < panelIcons.Count; i++)
        {
            panelIcons[i].GetComponent<Image>().sprite = icons[iconsIndex[i]].GetComponent<SpriteRenderer>().sprite;
            print(panelIcons[i]);
        }
    }

    public void SaveGame()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "notifications.txt");

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            bool a = true;
            foreach (int element in iconsIndex)
            {
                if (a)
                {
                    a = false;
                }
                else
                {
                    writer.Write(" ");
                }
                writer.Write(element);
            }
            writer.WriteLine();
        }

        for (int i = 0; i < panelIcons.Count; i++)
        {
            panelIcons[i].GetComponent<Image>().sprite = icons[iconsIndex[i]].GetComponent<SpriteRenderer>().sprite;
            print(panelIcons[i]);
        }
    }

    void ReadFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "notifications.txt");

        StreamReader reader = new StreamReader(filePath);
        string line;

        while ((line = reader.ReadLine()) != null)
        {
            string[] stringValues = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < stringValues.Length; i++)
            {
                iconsIndex[i] = Convert.ToInt32(stringValues[i]);
            }
        }
    }
}
