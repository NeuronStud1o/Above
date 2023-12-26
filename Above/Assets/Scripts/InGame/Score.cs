using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform player;
    public static Score instance;
    public Text scoreText;

    void Start()
    {
        instance = this;
    }

    private void Update()
    {
        scoreText.text = ((int)(player.position.y)).ToString();
    }
}
