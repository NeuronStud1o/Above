using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SpawnObstKoefManager
{
    public int maxScoreValue;
    public int minScoreValue;

    public float minKoef;
    public float maxKoef;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private List<SpawnObstKoefManager> listOfKoefs;
    [SerializeField] private Transform deadLineForObstacles;

    [SerializeField] private float distance = 0;

    public bool isFirstSpawner = true;

    public static Spawner spawner1;
    public static Spawner spawner2;

    Vector2 SpawnPos = new Vector2();

    void Awake()
    {
        if (isFirstSpawner == true)
        {
            spawner1 = this;
        }
        else
        {
            spawner2 = this;
        }
    }

    private void Start()
    {
        listOfKoefs = Score.instance.ListOfKoefs;
        SpawnObstacles();
    }

    private void Update()
    {
        if (transform.position.y > distance)
        {
            SpawnObstacles();
        }
    }

    private void SpawnObstacles()
    {
        (float minKoef, float maxKoef) = ChangeKoef();
        int countOfIterations = 0;

        for (int i = 0; i < 5; i++)
        {
            countOfIterations++;

            float additionalDistance = UnityEngine.Random.Range(minKoef, maxKoef);
            SpawnPos.x = transform.position.x;

            GameObject Empty = enemies[UnityEngine.Random.Range(0, enemies.Length)];

            if (additionalDistance + SpawnPos.y > deadLineForObstacles.position.y)
            {
                return;
            }

            SpawnPos.y += additionalDistance;

            if (countOfIterations <= 4)
            {
                distance = SpawnPos.y - 7;
            }

            Instantiate(Empty, SpawnPos, Quaternion.identity);
        }
    }

    private (float min, float max) ChangeKoef()
    {
        int valueScore = Convert.ToInt32(Score.instance.scoreText.text);

        foreach (SpawnObstKoefManager k in listOfKoefs)
        {
            if (k.minScoreValue <= valueScore && k.maxScoreValue > valueScore)
            {
                return (k.minKoef, k.maxKoef);
            } 
        }

        return (listOfKoefs[listOfKoefs.Count - 1].minKoef, listOfKoefs[listOfKoefs.Count - 1].maxKoef);
    }
}