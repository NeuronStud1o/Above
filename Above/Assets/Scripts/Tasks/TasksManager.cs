using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static IsActiveButton;

public class TasksManager : MonoBehaviour
{
    public int tasksCount;

    public static bool[] tasks = new bool[10];
    public GameObject[] panels;

    void Start()
    {
        if (PlayerPrefs.GetInt("FirstTimeInGame") == 0)
        {
            SaveGame();
            PlayerPrefs.SetInt("FirstTimeInGame", 1);
        }

        ReadFile();

        for (int i = 0; i < panels.Length; i++)
        {
            if (tasks[i] == true)
            {
                panels[i].SetActive(true);
            }
        }
    }

    public static void SaveGame()
    {
        //string rootPath = Application.dataPath;
        string filePath = Path.Combine(Application.persistentDataPath, "tasks.txt");

        // Відкриваємо файл для запису
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            bool a = true;
            // Записуємо кожен елемент масиву в окремий рядок
            foreach (bool element in tasks)
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
    }

    void ReadFile()
    {
        //string rootPath = Application.dataPath;
        string filePath = Path.Combine(Application.persistentDataPath, "tasks.txt");

        // Відкриваємо файл для читання
        StreamReader reader = new StreamReader(filePath);
        string line;

        // Зчитуємо рядки з файлу по одному
        while ((line = reader.ReadLine()) != null)
        {
            string[] stringValues = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < stringValues.Length; i++)
            {
                tasks[i] = Convert.ToBoolean(stringValues[i]);
            }
        }
    }
}
