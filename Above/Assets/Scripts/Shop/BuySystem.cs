using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BuySystem : MonoBehaviour
{
    public int skinsCount;
    public int bgsCount;
    public int boostsCount;

    public bool[] elements = new bool[8];
    public bool[] elements2 = new bool[5];
    public bool[] elements3 = new bool[4];

    public static bool[][] resultElements = new bool[3][];

    void Start()
    {
        if (PlayerPrefs.GetInt("FirstTimeInGameShop") == 0)
        {
            SaveGame();
            PlayerPrefs.SetInt("FirstTimeInGameShop", 1);
        }

        ReadData();

        elements[0] = true;
        elements2[0] = true;
        elements3[0] = true;

        if (!PlayerPrefs.HasKey("ShopItems"))
        {
            for (int i = 1; i < skinsCount; i++)
            {
                elements[i] = false;
            }
            for (int i = 1; i < bgsCount; i++)
            {
                elements2[i] = false;
            }
            for (int i = 1; i < boostsCount; i++)
            {
                elements3[i] = false;
            }
            PlayerPrefs.SetInt("ShopItems", 1);
        }
    }

    private void ReadData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "file.txt");

        if (File.Exists(filePath))
        {
            ReadFile();
        }
        else
        {
            SaveGame();
        }
    }

    public void SaveGame()
    {
        string fileName = "file.txt";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            bool a = true;
            foreach (bool element in elements)
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

            bool b = true;

            foreach (bool element in elements2)
            {
                if (b)
                {
                    b = false;
                }
                else
                {
                    writer.Write(" ");
                }
                writer.Write(element);
            }
            writer.WriteLine();

            bool c = true;

            foreach (bool element in elements3)
            {
                if (c)
                {
                    c = false;
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
        string filePath = Path.Combine(Application.persistentDataPath, "file.txt");

        StreamReader reader = new StreamReader(filePath);
        string line;

        int indexShop = 0;
        while ((line = reader.ReadLine()) != null)
        {
            string[] stringValues = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            resultElements[indexShop] = new bool[stringValues.Length];

            for (int i = 0; i < stringValues.Length; i++)
            {
                resultElements[indexShop][i] = Convert.ToBoolean(stringValues[i]);
            }
            indexShop++;
        }

        for (int i = 0; i < elements.Length; i++)
        {
            elements[i] = resultElements[0][i];
        }

        for (int i = 0; i < elements2.Length; i++)
        {
            elements2[i] = resultElements[1][i];
        }

        for (int i = 0; i < elements3.Length; i++)
        {
            elements3[i] = resultElements[2][i];
        }
    }
}
