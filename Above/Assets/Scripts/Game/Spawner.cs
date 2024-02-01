using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemy;

    [SerializeField] private float Distance = 20;
    [SerializeField] private float AddNewDistance = 25;

    Vector2 SpawnPos = new Vector2();

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject Empty = Enemy[Random.Range(0, Enemy.Length)];

            SpawnPos.x = transform.position.x;
            SpawnPos.y += Random.Range(4f, 10f);

            Instantiate(Empty, SpawnPos, Quaternion.identity);
        }
    }

    private void Update()
    {
        if (transform.position.y > Distance)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject Empty = Enemy[Random.Range(0, Enemy.Length)];

                SpawnPos.x = transform.position.x;
                SpawnPos.y += Random.Range(4f, 10f);

                Instantiate(Empty, SpawnPos, Quaternion.identity);
            }

            Distance += AddNewDistance;
        }
    }
}
