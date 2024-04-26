using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    private int i;

    [SerializeField] private GameObject[] AllCharacters;

    [SerializeField] private GameObject[] EquipButtons;
    [SerializeField] private GameObject[] EquipedButtons;

    void Start()
    {
        i = JsonStorage.instance.data.currentShop.currentSkin;

        EquipedButtons[i].SetActive(true);
        EquipButtons[i].SetActive(false);

        AllCharacters[i].SetActive(true);
    }

    public void Change(int thisCharacter)
    {
        for (int i = 0; i < AllCharacters.Length; i++)
        {
            AllCharacters[i].SetActive(false);
            EquipedButtons[i].SetActive(false);
            EquipButtons[i].SetActive(true);
        }

        JsonStorage.instance.data.currentShop.currentSkin = thisCharacter;

        CryptoHelper.Encrypt(JsonStorage.instance.data, JsonStorage.instance.password);

        AllCharacters[thisCharacter].SetActive(true);

        EquipedButtons[thisCharacter].SetActive(true);
        EquipButtons[thisCharacter].SetActive(false);
    }
}
