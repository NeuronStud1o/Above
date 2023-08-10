using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSFX : MonoBehaviour
{
    public AudioSource myFX;
    public AudioClip clickFX;
    
    public void ClickSound()
    {
        myFX.PlayOneShot(clickFX);
    }
}
