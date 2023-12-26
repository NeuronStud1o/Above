using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OtherSettings : MonoBehaviour
{
    [SerializeField] private GameObject levelRanks;
    [SerializeField] private List<GameObject> particles;

    public void OpenDocumentation()
    {
        Application.OpenURL("https://docs.google.com/document/d/1ANJd7XmfpLubOtsssmON4Fuc_4nhoc4LQUoeockF8RY/edit?usp=sharing");
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

    public void AutoSaveSettings(bool tog)
    {
        if (tog == true)
        {
            
        }
        else
        {
            
        }
    }

    public void PlayTutorial()
    {
        SceneManager.LoadSceneAsync("Tutorial");
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

        }
        else
        {

        }
    }

    public void CameraShake(bool tog)
    {
        if (tog == true)
        {

        }
        else
        {

        }
    }

    public void Vibration(bool tog)
    {
        if (tog == true)
        {

        }
        else
        {

        }
    }
}
