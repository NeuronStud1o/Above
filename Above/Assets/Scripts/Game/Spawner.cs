using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemy;

    [SerializeField] private float Distance = 20;
    [SerializeField] private float AddNewDistance = 25;

    public float koef = 9;
    public float minKoef = 0;

    Vector2 SpawnPos = new Vector2();

    private void Start()
    {
        ChangeKoef();

        for (int i = 0; i < 5; i++)
        {
            GameObject Empty = Enemy[UnityEngine.Random.Range(0, Enemy.Length)];

            SpawnPos.x = transform.position.x;
            SpawnPos.y += UnityEngine.Random.Range(minKoef, koef);

            Instantiate(Empty, SpawnPos, Quaternion.identity);
        }
    }

    private void Update()
    {
        if (transform.position.y > Distance)
        {
            ChangeKoef();

            if (minKoef < 3)
            {
                minKoef = 3;
            }

            for (int i = 0; i < 5; i++)
            {
                GameObject Empty = Enemy[UnityEngine.Random.Range(0, Enemy.Length)];

                SpawnPos.x = transform.position.x;

                SpawnPos.y += UnityEngine.Random.Range(minKoef, koef);

                Instantiate(Empty, SpawnPos, Quaternion.identity);
            }

            Distance += AddNewDistance;
        }
    }

    private void ChangeKoef()
    {
        int valueScore = Convert.ToInt32(Score.instance.scoreText.text);

        float k = 9;

        for (int i = 0; i < 200; i += 20)
        {
            if (k <= 5)
            {
                return;
            }

            if (valueScore < i)
            {
                koef = k;
                minKoef = ((Mathf.Pow(koef, 2) - Mathf.Pow(koef - 3, 2)) / 10);
                return;
            }

            k -= 0.5f;
        }
    }
}
