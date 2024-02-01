using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public static CoinSpawner instance;
    [SerializeField] private GameObject FlyCoin;
    [SerializeField] private GameObject SuperCoin;

    [SerializeField] private GameObject LineSpawnFlyCoin;
    [SerializeField] private GameObject LineSpawnSuperCoin;
    public GameObject hero;

    void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (hero != null)
        {
            if (hero.transform.position.y > LineSpawnFlyCoin.transform.position.y)
            {
                SpawnFlyCoin();
            }

            if (hero.transform.position.y > LineSpawnSuperCoin.transform.position.y)
            {
                SpawnSuperCoin();
            }
        }
    }

    void SpawnFlyCoin()
    {
        Vector2 SpawnPos = new Vector2();
        SpawnPos = LineSpawnFlyCoin.transform.position;

        for (int i = 0; i < 5; i++)
        {
            SpawnPos.x = Random.Range(-1, 1);
            SpawnPos.y += Random.Range(7f, 50f);

            GameObject coin = Instantiate(FlyCoin, SpawnPos, Quaternion.identity);

            Vector2 LineVector = new Vector2();
            LineVector = LineSpawnFlyCoin.transform.position;
            LineVector.y = coin.transform.position.y;

            LineSpawnFlyCoin.transform.position = LineVector;
        }
    }

    void SpawnSuperCoin()
    {
        Vector2 SpawnPos = new Vector2();
        SpawnPos = LineSpawnSuperCoin.transform.position;

        for (int i = 0; i < 2; i++)
        {
            SpawnPos.x = Random.Range(-1, 1);
            SpawnPos.y += Random.Range(50f, 500f);

            GameObject coin = Instantiate(SuperCoin, SpawnPos, Quaternion.identity);

            Vector2 LineVector = new Vector2();
            LineVector = LineSpawnSuperCoin.transform.position;
            LineVector.y = coin.transform.position.y;

            LineSpawnSuperCoin.transform.position = LineVector;
        }
    }
}

