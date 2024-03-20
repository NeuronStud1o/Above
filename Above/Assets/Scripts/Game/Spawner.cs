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

            Debug.Log(koef + " IS KOEF!!!");

            if (minKoef < 3.2)
            {
                minKoef = 3.2f;
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

        if (valueScore > 400 && valueScore < 600)
        {
            // 300-699 = connoisseur
            koef = 5.9f;
            minKoef = ((Mathf.Pow(koef, 2) - Mathf.Pow(koef - 3, 2)) / 10);

            AddNewDistance = 24f;

            return;
        }

        if (valueScore > 700 && valueScore < 950)
        {
            // 700-999 = experienced
            koef = 5.8f;
            minKoef = ((Mathf.Pow(koef, 2) - Mathf.Pow(koef - 3, 2)) / 10);

            AddNewDistance = 23.5f;

            return;
        }

        if (valueScore > 1000)
        {
            // >=1000 = vip
            koef = 5.7f;
            minKoef = ((Mathf.Pow(koef, 2) - Mathf.Pow(koef - 3, 2)) / 10);

            AddNewDistance = 23f;

            return;
        }

        for (int i = 0; i < 350; i += 50)
        {
            // 0-99 = beginer
            // 100-199 = flyer
            // 200-299 = pro

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
