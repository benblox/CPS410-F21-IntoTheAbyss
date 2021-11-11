using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subLights : MonoBehaviour
{
    private GameObject frontLights;
    private AudioSource OnAS;
    private AudioSource HumAS;
    public float fadeOutTime = 0.6f;
    public AudioClip lightsOn;
    public AudioClip lightsHum;
    private float startVolume;
    
    void Start()
    {
        frontLights = transform.GetChild(0).gameObject;
        OnAS = transform.GetChild(1).GetComponent<AudioSource>();
        HumAS = transform.GetChild(2).GetComponent<AudioSource>();
        startVolume = OnAS.volume;
    }

    public void setLights(bool lights)
    {
        if (lights) turnOn();
        else turnOff();
    }
    public void turnOn()
    {
        frontLights.SetActive(true);
        OnAS.Play();
        HumAS.PlayDelayed(0.1f);
    }
    public void turnOff()
    {
        frontLights.SetActive(false);
        OnAS.Stop();
        StartCoroutine(fadeOutSound());
    }


    IEnumerator fadeOutSound()
    {
        while (HumAS.volume > 0)
        {
            HumAS.volume -= startVolume * Time.deltaTime / fadeOutTime;
            yield return null;
        }
        HumAS.Stop();
        HumAS.volume = startVolume;
    }
}
