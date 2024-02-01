using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform doodlePos;
    [SerializeField] private GameObject boost;

    void Start()
    {
        if (!JsonStorage.instance.jsonData.otherSettings.showSelectedBoostInGame)
        {
            boost.SetActive(false);
        }
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
