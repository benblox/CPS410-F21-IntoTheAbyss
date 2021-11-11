using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubBattery : MonoBehaviour
{
    private float charge;
    private float drain;
    private int extraBatteries;
    private bool batteryHasCharge;
    private bool lightsOn = false;
    private bool isBoosting = false;
    new private AudioSource audio;
    private Rigidbody sub;

    public AudioClip batteryReplaceSound;
    public Slider slider;
    public Image batteryColor;
    private subLights subLights;
    public GameObject indicatorLight;
    public TextMeshProUGUI numBatts;
    public float lightDrainAmount = 0.01f;
    public float boostDrainAmount = 0.75f;


    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        subLights = GetComponentInChildren<subLights>();
        sub = GetComponent<Rigidbody>();
    }
    void Start()
    {
        charge = 100f;
        batteryHasCharge = true;
        drain = 0f;
        extraBatteries = 3;
    }

    void FixedUpdate()
    {
        if (batteryHasCharge)
        {
            drainBattery();
            updateSlider();
        }
        //else powerOff();

        if (isBoosting)
        {
            sub.AddForce(transform.forward *
                transform.GetComponent<submarine>().speedForce * 2.75f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Chest")
        {
            addBattery();
        }
    }

    private void drainBattery()
    {
        drain = 0f;

        //check what's using power
        if (lightsOn) drain += lightDrainAmount;
        if (isBoosting) drain += boostDrainAmount;
        //others go here...

        //set battery indicator light
        indicatorLight.SetActive(drain > 0f);

        //drain power from battery
        charge = Mathf.Clamp(charge - drain, 0f, 100f);

        //Replace battery
        if (charge == 0f && extraBatteries > 0)
        {
            powerOff();
            batteryHasCharge = false;
            StartCoroutine(ReplaceBattery(1f));
        }//no batteries left
        else if (charge == 0f && extraBatteries == 0)
        {
            powerOff();
        }
    }

    private void updateSlider()
    {
        //Update Slider value
        slider.value = charge;
        numBatts.text = extraBatteries.ToString();

        //Change Battery Slider color (RGBA)
        if (charge < 3f) batteryColor.color = new Color32(255, 0, 0, 255);
        else if (charge >= 50f)
        {
            batteryColor.color = new Color32
                ((byte)Mathf.Floor(Mathf.Clamp(255 - ((charge - 50f) * 5), 0f, 250f)),
                255,
                0, 255);
        }
        else
        {
            batteryColor.color = new Color32
                (255,
                (byte)Mathf.Floor(charge * 5),
                0, 255);
        }
    }

    IEnumerator ReplaceBattery(float seconds)
    {
        //print("replacing battery");
        extraBatteries -= 1;
        audio.PlayOneShot(batteryReplaceSound, 0.5f);
        float time = 0f;
        while (time < seconds)
        {
            time += Time.deltaTime;
            float lerpVal = time / seconds;
            slider.value = Mathf.Lerp(slider.value, 100f, lerpVal);
            yield return null;
        }
        //flash the indicator light
        for (int i = 0; i < 3; i++)
        {
            indicatorLight.SetActive(true);
            yield return new WaitForSeconds(0.15f);
            indicatorLight.SetActive(false);
            yield return new WaitForSeconds(0.15f);
        }
        charge = 100f;
        batteryHasCharge = true;
    }

    private void powerOff()
    {
        //Lights
        indicatorLight.SetActive(false);
        lightsOn = false;
        isBoosting = false;
        subLights.setLights(false);
        batteryHasCharge = false;
    }

    public void addBattery()
    {
        extraBatteries += 1;
        if (!batteryHasCharge) StartCoroutine(ReplaceBattery(1f));
    }
    public void toggleLights()
    {
        if(charge > 0f)
        {
            subLights.setLights(!lightsOn);
            lightsOn = !lightsOn;
        }
    }
    public void useBoost()
    {
        if (charge > 0f)
        {
            isBoosting = true;
        }
    }
    public void boostOff()
    {
        isBoosting = false;
    }
}
