using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemy;
    [SerializeField] private float distance = 0;
    [SerializeField] private float koef = 8f;
    [SerializeField] private float minKoef = 4f;

    [SerializeField] private Transform deadLineForObstacles;

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
        ChangeKoef();

        int countOfIterations = 0;

        for (int i = 0; i < 5; i++)
        {
            countOfIterations++;

            GameObject Empty = Enemy[UnityEngine.Random.Range(0, Enemy.Length)];

            SpawnPos.x = transform.position.x;

            float additionalDistance = UnityEngine.Random.Range(minKoef, koef);

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

    private void ChangeKoef()
    {
        int valueScore = Convert.ToInt32(Score.instance.scoreText.text);

        float k = 7.5f;

        if (valueScore > 500 && valueScore < 700)
        {
            koef = 5.2f;
            minKoef = 3.1f;

            Debug.Log(koef + " IS KOEF!!!");

            return;
        }

        if (valueScore > 700 && valueScore < 1000)
        {
            koef = 5f;
            minKoef = 3.1f;

            Debug.Log(koef + " IS KOEF!!!");

            return;
        }

        if (valueScore > 1000)
        {
            koef = 4.8f;
            minKoef = 3.1f;

            Debug.Log(koef + " IS KOEF!!!");

            return;
        }

        for (int i = 0; i < 400; i += 50)
        {
            if (valueScore > i)
            {
                k -= 0.25f;
            }
        }

        koef = k;

        if (k / 2 > 3.2f)
        {
            minKoef = k / 2f;
        }
        else
        {
            minKoef = 3.2f;
        }
        
        Debug.Log(koef + " IS KOEF!!!");
    }
}