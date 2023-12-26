using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class StartOnClick : MonoBehaviour
{
    public static StartOnClick instance;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject button;
    [SerializeField] private Button pauseButton;
    [SerializeField] private EventTrigger triggerEvent;

    public Player player;

    [SerializeField] private GameObject destroyPoint;

    void Awake()
    {
        DestroyPoint.destroyPoint = destroyPoint;
    }

    void Start()
    {
        instance = this;
    }

    public void StartGame()
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;

        entry.callback.AddListener((eventData) => { YourPointerDownMethod(); });
        triggerEvent.triggers.Add(entry);

        player.isCanMove = true;
        pauseButton.interactable = true;

        text.enabled = false;
        button.SetActive(false);
    }

    private void YourPointerDownMethod()
    {
        player.Jump();
    }
}
