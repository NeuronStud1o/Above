using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorWalls : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private Transform generatorPoint;
    [SerializeField] private float offset;
    [SerializeField] private float additionalDistance;

    float backgroundHeight;

    void Start()
    {
        backgroundHeight = wall.GetComponentInChildren<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        if (transform.position.y + offset < generatorPoint.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + backgroundHeight + additionalDistance, transform.position.z);

            Instantiate(wall, transform.position, transform.rotation);
        }
    }
}
