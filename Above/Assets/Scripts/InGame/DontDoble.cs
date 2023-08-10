using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDoble : MonoBehaviour
{
    Vector2 SpawnPos = new Vector2();

    private void Start()
    {
        SpawnPos = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            SpawnPos.y += 2f;
        }
    }
}
