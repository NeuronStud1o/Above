using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartOnClick : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject button;
    public Button pauseButton;

    public static Player player;

    void Start()
    {
        player.enabled = false;
    }

   public void StartGame()
   {
        player.enabled = true;
        pauseButton.interactable = true;

        text.enabled = false;
        button.SetActive(false);
   }

}
