using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavyCutsceneSoundScript : MonoBehaviour
{
    public AudioSource captainSound;
    public AudioSource bradshawSound;
    public AudioSource westonSound;

    public void playCaptainSound()
    {
        captainSound.Play(0);
    }

    public void playBradshawSound()
    {
        bradshawSound.Play(0);
    }

    public void playWestonSound()
    {
        westonSound.Play(0);
    }

}
