using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform player;
    public static Score instance;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        instance = this;
    }

    private void Update()
    {
        scoreText.text = ((int)(player.position.y)).ToString();
    }
}
