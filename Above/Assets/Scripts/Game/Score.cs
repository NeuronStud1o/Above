using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform player;
    public static Score instance;
    public TextMeshProUGUI scoreText;

    [SerializeField] private List<SpawnObstKoefManager> listOfKoefs;
    public List<SpawnObstKoefManager> ListOfKoefs
    {
        get => listOfKoefs;
        private set => listOfKoefs = value;
    }

    void Start()
    {
        instance = this;
    }

    private void Update()
    {
        scoreText.text = ((int)(player.position.y)).ToString();
    }
}
