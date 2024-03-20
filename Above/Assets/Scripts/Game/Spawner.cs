using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemy;

    [SerializeField] private float distance = 0;
    [SerializeField] private float addNewDistance = 25f;

    public bool isFirstSpawner = true;

    public float lastObstacleY = 0;

    public float koef = 8f;
    public float minKoef = 4f;

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
        //ChangeKoef();

        koef = 5f;
        minKoef = 3.1f;
        addNewDistance = 19.8f;

        for (int i = 0; i < 5; i++)
        {
            GameObject Empty = Enemy[UnityEngine.Random.Range(0, Enemy.Length)];

            SpawnPos.x = transform.position.x;
            SpawnPos.y += UnityEngine.Random.Range(minKoef, koef);

            Instantiate(Empty, SpawnPos, Quaternion.identity);

            lastObstacleY = SpawnPos.y;
        }

        if (spawner1.lastObstacleY - 50 > spawner2.lastObstacleY && isFirstSpawner ||
         spawner2.lastObstacleY - 50 > spawner1.lastObstacleY && !isFirstSpawner)
        {
            distance += Mathf.Round(addNewDistance);
        }

        distance += Mathf.Round(addNewDistance);
    }

    private void ChangeKoef()
    {
        int valueScore = Convert.ToInt32(Score.instance.scoreText.text);

        float k = 7.5f;
        float newDistance = 25f;

        if (valueScore > 500 && valueScore < 700)
        {
            koef = 5.3f;
            minKoef = 3.2f;
            addNewDistance = 19f;

            Debug.Log(koef + " IS KOEF!!!");

            return;
        }

        if (valueScore > 700 && valueScore < 1000)
        {
            koef = 5f;
            minKoef = 3.3f;
            addNewDistance = 19.8f;

            Debug.Log(koef + " IS KOEF!!!");

            return;
        }

        if (valueScore > 1000)
        {
            koef = 4.8f;
            minKoef = 3.3f;
            addNewDistance = 19.6f;

            Debug.Log(koef + " IS KOEF!!!");

            return;
        }

        for (int i = 0; i < 400; i += 50)
        {
            if (valueScore > i)
            {
                k -= 0.25f;
                newDistance -= 0.5f;
            }
        }

        addNewDistance = newDistance;
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
