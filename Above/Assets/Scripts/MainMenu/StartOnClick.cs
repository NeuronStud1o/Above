using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartOnClick : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject button;
    [SerializeField] private Button pauseButton;

    public Player player;

    [SerializeField] private GameObject destroyPoint;

    void Awake()
    {
        DestroyPoint.destroyPoint = destroyPoint;
    }

    public void StartGame()
    {
        player.enabled = true;
        pauseButton.interactable = true;

        text.enabled = false;
        button.SetActive(false);
    }
}
