using Gaia;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script controls everything for the ocean cutscene.
//It builds references to the things it needs in Start().
//The methods in this script get called from animation events found in the 'FlyCamOceanCutscene' animation.
//The animator that plays this animation is attached to the same GameObject this script is.
public class OceanCutscene : MonoBehaviour
{
    private Transform flyCam;
    private Transform waterSurface;
    private float startingY;
    private float endY = 350f;
    private Light sun;
    private float sunMaxIntensity;
    private float depth;
    private AudioSource sound;
    private Animator Crasc;
    private GameObject staticScreen;
    private CanvasGroup Logo;
    private CanvasGroup blackScreen;
    private Animator shark;

    public AudioClip bite;

    void Start()
    {
        flyCam = transform.GetChild(0);
        sound = flyCam.GetComponent<AudioSource>();
        waterSurface = GameObject.FindGameObjectWithTag("Water").transform;
        startingY = waterSurface.position.y;
        sun = GameObject.Find("Gaia Lighting/Directional Light").GetComponent<Light>();
        sunMaxIntensity = sun.intensity;
        Crasc = transform.GetChild(3).GetComponent<Animator>();
        staticScreen = transform.GetChild(4).gameObject;
        Logo = transform.GetChild(5).GetComponent<CanvasGroup>();
        blackScreen = transform.GetChild(8).GetComponent<CanvasGroup>();
        shark = transform.GetChild(6).GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("0"))
        {
            Time.timeScale = .5f;
        }
        if (Input.GetKeyDown("1"))
        {
            Time.timeScale = 1f;
        }
        if (Input.GetKeyDown("2"))
        {
            Time.timeScale = 2f;
        }
        if (Input.GetKeyDown("3"))
        {
            Time.timeScale = 3f;
        }
    }

    void FixedUpdate()
    {
        depth = (waterSurface.position.y - flyCam.position.y);
        sun.intensity = (1f - Mathf.Clamp01(depth / 200f)) * sunMaxIntensity;
    }

    IEnumerator moveWater()
    {
        while (waterSurface.position.y < endY)
        {
            waterSurface.position = new Vector3(0f, waterSurface.position.y + 7);
            yield return new WaitForSeconds(0.1f);
        }
        print("done");
    }

    public void playAudio()
    {
        Time.timeScale = 1f;
        sound.Play();
    }

    public void crascBite()
    {
        Crasc.Play("Base Layer.lungeBiteForward", 0, 0);
    }

    public void sharkBite()
    {
        shark.Play("New Layer.eat", 0, 0);
        sound.PlayOneShot(bite);
    }

    //So theres a wierd bug with playing the static video..
    //The video is not even a second long and it loops.
    //For some reason, during the first loop, it drops a couple frames 
    //so the video essentially disappears for that time. It only does this on the first loop.
    //This method sets the video transparency to 0 (invisible) and lets it play for a couple seconds.
    //Then it sets the transparency back to 1 so when the video appears it doesn't skip anything.
    IEnumerator playStatic()
    {
        staticScreen.GetComponent<UnityEngine.Video.VideoPlayer>().targetCameraAlpha = 0f;
        staticScreen.SetActive(true);
        yield return new WaitForSeconds(1.4f);
        staticScreen.GetComponent<UnityEngine.Video.VideoPlayer>().targetCameraAlpha = 1f;
        yield return new WaitForSeconds(1.4f);
        StartCoroutine(fadeIn(Logo));
    }

    //Fades in canvasGroup
    IEnumerator fadeIn(CanvasGroup cg)
    {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(1f / 30f);
            cg.alpha += 1f / 30f;
        }
    }

    public void fadeInLogo()
    {
        StartCoroutine(fadeIn(Logo));
    }

    public void fadeInBlack()
    {
        StartCoroutine(fadeIn(blackScreen));
    }

    IEnumerator fadeOutMusic()
    {
        AudioSource underwaterAudio = GameObject.Find("Underwater Effects").GetComponent<AudioSource>();

        for (int i = 0; i < 50; i += 1)
        {
            yield return new WaitForSeconds(1f/30f);
            underwaterAudio.volume -= 0.01f;
        }
        underwaterAudio.volume = 0;
    }

    public void turnOffGaiaAudio()
    {
        GameObject ga = GameObject.Find("Gaia Audio");
        ga.SetActive(false);
    }
}
