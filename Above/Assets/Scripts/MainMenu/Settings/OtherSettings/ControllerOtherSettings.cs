using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerOtherSettings : MonoBehaviour
{
    [Header ("## Menu settings : ")]
    [SerializeField] private GameObject levelRanks;
    [SerializeField] private GameObject boost;
    [SerializeField] private List<MonoBehaviour> autoSaveScripts;
    [SerializeField] private List<GameObject> particles;

    void Start()
    {
        ShowLevelRanks(JsonStorage.instance.data.otherSettings.showLevelRanks);
        ActivateParticles(JsonStorage.instance.data.otherSettings.particles);
        ShowTheSelectedBoost(JsonStorage.instance.data.otherSettings.showSelectedBoostInGame);
    }

    public void ShowLevelRanks(bool tog)
    {
        if (tog == true)
        {
            levelRanks.SetActive(true);
        }
        else
        {
            levelRanks.SetActive(false);
        }
    }

    public void ActivateParticles(bool tog)
    {
        if (tog == true)
        {
            foreach (GameObject go in particles)
            {
                go.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject go in particles)
            {
                go.SetActive(false);
            }
        }
    }

    public void ShowTheSelectedBoost(bool tog)
    {
        if (tog == true)
        {
            boost.SetActive(true);
        }
        else
        {
            boost.SetActive(false);
        }
    }
}
