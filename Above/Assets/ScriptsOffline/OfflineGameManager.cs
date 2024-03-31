using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OfflineGameManager : MonoBehaviour
{
    public static OfflineGameManager instance;
    [SerializeField] private List<GameObject> levels;
    [SerializeField] private List<GameObject> heroes;
    [SerializeField] private TextMeshProUGUI coinsF;
    [SerializeField] private TextMeshProUGUI coinsS;
    [SerializeField] private TextMeshProUGUI level;

    IEnumerator Start()
    {
        instance = this;

        levels[LevelManager.instance.eqipedLevel - 1].SetActive(true);
        heroes[LevelManager.instance.equipedHero].SetActive(true);
        
        CameraFollowOffline.instance.doodlePos = heroes[LevelManager.instance.equipedHero].transform;

        coinsF.text = PlayerPrefs.GetInt("coinsF") + "";
        coinsS.text = PlayerPrefs.GetInt("coinsS") + "";
        level.text = "Level: " + LevelManager.instance.eqipedLevel;

        yield return new WaitForSeconds(0.5f);

        Camera.main.GetComponent<AudioSource>().enabled = true;
    }

    public void UpdateUI()
    {
        Debug.Log(1);
        coinsF.text = PlayerPrefs.GetInt("coinsF") + "";
        coinsS.text = PlayerPrefs.GetInt("coinsS") + "";
        level.text = "Level: " + LevelManager.instance.eqipedLevel; 
    }
}
