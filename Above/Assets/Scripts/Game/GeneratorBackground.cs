using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBackground : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private Transform generatorPoint;
    [SerializeField] private float offset;

    float backgroundHeight;

    [SerializeField] private AudioClip audioClip;

    void Start()
    {
        Camera.main.GetComponent<AudioSource>().clip = audioClip;

        backgroundHeight = background.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        if (transform.position.y + offset < generatorPoint.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + backgroundHeight, transform.position.z);

            Instantiate(background, transform.position, transform.rotation);
        }
    }
}
