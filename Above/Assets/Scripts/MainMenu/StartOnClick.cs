using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartOnClick : MonoBehaviour
{
    public Text text;
    public GameObject button;
    public Button pauseButton;

   public void StartGame()
   {
        Buttons.Hero.GetComponent<Player>().enabled = true;
        pauseButton.interactable = true;

        text.enabled = false;
        button.SetActive(false);
   }

}
