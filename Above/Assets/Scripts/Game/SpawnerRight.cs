using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRight : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemy;

    [SerializeField] private float Distance = 20;
    [SerializeField] private float AddNewDistance = 25;

    public int koef = 10;

    Vector2 SpawnPos = new Vector2();

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject Empty = Enemy[UnityEngine.Random.Range(0, Enemy.Length)];

            SpawnPos.x = transform.position.x;
            SpawnPos.y += UnityEngine.Random.Range(koef - 4, koef);

            Instantiate(Empty, SpawnPos, Quaternion.identity);
        }
    }

    private void Update()
    {
        if (transform.position.y > Distance)
        {
            ChangeKoef();

            for (int i = 0; i < 5; i++)
            {
                int min = koef;
                GameObject Empty = Enemy[UnityEngine.Random.Range(0, Enemy.Length)];

                SpawnPos.x = transform.position.x;

                if (koef - 4 < 3)
                {
                    min = 3;
                }

                SpawnPos.y += UnityEngine.Random.Range(min, koef);

                Instantiate(Empty, SpawnPos, Quaternion.identity);
            }

            Distance += AddNewDistance;
        }
    }

    private void ChangeKoef()
    {
        int valueScore = Convert.ToInt32(Score.instance.scoreText.text);

        int k = 10;

        for (int i = 0; i < 400; i += 40)
        {
            if (k < 5)
            {
                k = 5;
            }

            if (valueScore < i)
            {
                koef = k;
                return;
            }

            k--;
        }
    }
}