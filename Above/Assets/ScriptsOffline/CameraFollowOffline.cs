using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowOffline : MonoBehaviour
{
    public static CameraFollowOffline instance;
    public Transform doodlePos;
    
    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (doodlePos.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x,
                doodlePos.position.y, transform.position.z);
        }
    }
}
