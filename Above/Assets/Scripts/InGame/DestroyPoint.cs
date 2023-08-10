using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPoint : MonoBehaviour
{
    public GameObject destroyPoint;

    void Start()
    {
        destroyPoint = GameObject.Find("DestroyPoint");
    }

    void Update()
    {
        if (transform.position.y < destroyPoint.transform.position.y)
        {
            Destroy(gameObject);
        }
    }
}
