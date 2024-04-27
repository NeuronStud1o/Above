using UnityEngine;

public class SelectCharacterInGame : MonoBehaviour
{
    private int i = 0;

    [SerializeField] private GameObject[] AllCharacters;

    void Start()
    {
        i = JsonStorage.instance.data.currentShop.currentSkin;

        AllCharacters[i].SetActive(true);
    }
}
