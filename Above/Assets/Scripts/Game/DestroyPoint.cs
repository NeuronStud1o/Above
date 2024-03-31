using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPoint : MonoBehaviour
{
    public static GameObject destroyPoint;

    void Update()
    {
        if (transform.position.y < destroyPoint.transform.position.y)
        {
            Destroy(gameObject);
        }
    }
}
