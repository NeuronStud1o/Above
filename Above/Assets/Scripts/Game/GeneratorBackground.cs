using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBackground : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private Transform generatorPoint;
    [SerializeField] private float distanceBetween;

    float platformWidth;

    [SerializeField] private GameObject Camera;
    [SerializeField] private AudioClip audioClip;

    void Start()
    {
        Camera.GetComponent<AudioSource>().clip = audioClip;
        platformWidth = background.GetComponent<BoxCollider2D>().size.y;
    }

    void Update()
    {
        if (transform.position.y < generatorPoint.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + platformWidth + distanceBetween, transform.position.z);

            Instantiate(background, transform.position, transform.rotation);
        }
    }
}
