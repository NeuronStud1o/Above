using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NotificationsManager : MonoBehaviour
{
    public static NotificationsManager instance;
    [SerializeField] private GameObject[] icons;
    [SerializeField] private GameObject[] panelIcons;
    [SerializeField] private GameObject layout;
    public int[] iconsIndex = new int[3];

    Vector3 vector = new Vector3(1, 1, 1);

    void Start()
    {
        instance = this;

        StartCoroutine(Check());
    }

    private IEnumerator Check()
    {
        if (PlayerPrefs.GetInt("FirstTimeInGameForNotifications") == 0)
        {
            SaveGame();
            PlayerPrefs.SetInt("FirstTimeInGameForNotifications", 1);
            yield return null;
        }

        ReadFile();
    }

    public void RemoveIconWhithList(int index)
    {
        iconsIndex[index] = 0;
        for (int i = 0; i < iconsIndex.Length; i++)
        {
            if (iconsIndex[i] == index && panelIcons[i] != null)
            {
                Destroy(panelIcons[index]);
                panelIcons[i] = null;
                iconsIndex[i] = 0; // Зануліть індекс, щоб позначити, що об'єкт був знищений
            }
        }

        SaveGame();
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

        SetValue();
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

        SetValue();
    }

    void SetValue()
    {
        for (int i = 0; i < 3; i++)
        {
            print (iconsIndex[i]);
            if (iconsIndex[i] != 0)
            {
                GameObject icon = Instantiate(icons[iconsIndex[i]]);
                icon.transform.SetParent(layout.transform);
                icon.transform.localScale = vector;
                panelIcons[i] = icon;
            }
        }
    }
}