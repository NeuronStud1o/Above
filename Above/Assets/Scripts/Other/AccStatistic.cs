using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccStatistic : MonoBehaviour
{
    [Header ("## Panel parameters : ")]
    [SerializeField] private Image userIcon;
    [SerializeField] private TextMeshProUGUI flyCoinsAllTime;
    [SerializeField] private TextMeshProUGUI superCoinsAllTime;
    [SerializeField] private TextMeshProUGUI hightScore;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI createdAccount;

    [SerializeField] private TextMeshProUGUI userName;
    [SerializeField] private TextMeshProUGUI userEmail;

    [SerializeField] private GameObject panel;

    [Header ("## Other resources : ")]
    [SerializeField] private Image icon;

    public void OpenStatistic()
    {
        userIcon.sprite = icon.sprite;

        flyCoinsAllTime.text = JsonStorage.instance.data.userData.coinsFAllTime + "";
        superCoinsAllTime.text = JsonStorage.instance.data.userData.coinsSAllTime + "";
        hightScore.text = JsonStorage.instance.data.userData.record + "";
        level.text = JsonStorage.instance.data.userData.level + "";

        ulong creationTimestamp = UserData.instance.metadata.CreationTimestamp;
        long signedTimestamp = (long)creationTimestamp;

        DateTime creationDateTime = DateTimeOffset.FromUnixTimeMilliseconds(signedTimestamp).DateTime;
        string formattedDate = creationDateTime.ToString("yyyy-MM-dd");

        createdAccount.text = formattedDate;

        userName.text = UserData.instance.User.DisplayName;
        userEmail.text = UserData.instance.User.Email;

        panel.SetActive(true);
    }
}
