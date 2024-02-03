using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OfflineGameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> levels;
    [SerializeField] private List<GameObject> heroes;
    [SerializeField] private TextMeshProUGUI coinsF;
    [SerializeField] private TextMeshProUGUI coinsS;
    [SerializeField] private TextMeshProUGUI level;

    void Start()
    {
        levels[LevelManager.instance.eqipedLevel - 1].SetActive(true);
        heroes[LevelManager.instance.equipedHero].SetActive(true);
        
        CameraFollowOffline.instance.doodlePos = heroes[LevelManager.instance.equipedHero].transform;

        coinsF.text = PlayerPrefs.GetInt("coinsF") + "";
        coinsS.text = PlayerPrefs.GetInt("coinsS") + "";
        level.text = "Level: " + LevelManager.instance.eqipedLevel; 
    }
}
