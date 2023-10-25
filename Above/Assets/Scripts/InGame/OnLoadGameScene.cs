using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoadGameScene : MonoBehaviour
{
    [Header ("## Tutorial :")]
    [SerializeField] private List<GameObject> objectsToBeClosedInTutorial;
    [Space (10f)]
    [SerializeField] private GameObject tutorialGo;
    [SerializeField] private GameObject cameraGo;
    [SerializeField] private GameObject touchScreenArrow;

    private Animator camAnim;

    void Start()
    {
        camAnim = cameraGo.GetComponent<Animator>();
    
        if (PlayerPrefs.GetInt("StartFirstTime") == 0)
        {
            for (int i = 0; i < objectsToBeClosedInTutorial.Count; i++)
            {
                objectsToBeClosedInTutorial[i].SetActive(false);
            }
            
            tutorialGo.SetActive(true);
            camAnim.enabled = true;
        }
    }

    public void FirstJump()
    {
        StartCoroutine(AnimationOfJump());
    }

    IEnumerator AnimationOfJump()
    {
        camAnim.SetBool("FocusToHero", true);
        yield return new WaitForSeconds(2);
        touchScreenArrow.SetActive(true);
    }
}
