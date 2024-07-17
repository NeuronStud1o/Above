using TMPro;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager instance;

    public int coinsF;
    public int coinsS;

    [SerializeField] private TextMeshProUGUI moneyTextF;
    [SerializeField] private TextMeshProUGUI moneyTextS;

    void Start()
    {
        instance = this;

        coinsF = JsonStorage.instance.data.userData.coinsF;
        coinsS = JsonStorage.instance.data.userData.coinsS;

        UpdateUI();
    }

    public void UpdateUI()
    {
        moneyTextF.text = coinsF + "";
        moneyTextS.text = coinsS + "";
    }
}