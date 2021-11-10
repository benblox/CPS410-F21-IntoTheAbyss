using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *  SpawnFlare
 *  Created by: Brendan Roman, Into The Abyss
 * Purpose: To Instantiate flare that will scare monsters away
 * Variables:
 * flare : flare object
 * isShot: checks to see if flare has been shot
 * cooldown: time until next flare spawn available
 * _isPlaying: checks to see if audio is playing
 * audio: submarine Audio Source
 * sound: VA lines
 * shotPower: amount of force that is added to flare
 */
public class SpawnFlare : MonoBehaviour
{

    public GameObject flare;
    private bool isShot;
    public int cooldown;
    public Transform flaregun;
    private bool _isPlaying;
    new public AudioSource audio;
    public AudioClip sound;
    public int shotPower;
    public int ammo;
    public AudioClip noAmmo;


    
    // Start is called before the first frame update
    void Start()
    {
        isShot = false;
        //ammo = 5;
    }

    // Update is called once per frame


    /*
     * Update Method
     * Checks for keypress 
     * if submarine has flare ready, flare is shot. 
     */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isShot == true && _isPlaying == false)
            {
                print("true");
                StartCoroutine(CooldownSound());

            }
            else if (isShot == false && ammo >0)
            {
                Instantiate(flare, flaregun.position, flaregun.rotation).GetComponent<Rigidbody>().AddForce(flaregun.forward * shotPower);
                StartCoroutine(FlareCooldown());
                ammo--;
            }
            else
            {
                StartCoroutine(NoAmmoSound());
            }
        }
       
    }
    /*
     * FlareCooldown
     * Controls Cooldown for flare using isShot as bool
     */
    IEnumerator FlareCooldown()
    {
        isShot = true;
        yield return new WaitForSeconds(cooldown);
        isShot = false;
    }

    /*
     * CooldownSound
     * if on cooldown, submarine plays VA line
     */
    IEnumerator CooldownSound()
    {
        _isPlaying = true;
        audio.PlayOneShot(sound, 1.0f);
        yield return new WaitForSeconds(2);
        _isPlaying = false;
    }
    /*
     * NoAmmoSound
     * Play if no ammo
     */
    IEnumerator NoAmmoSound()
    {
        _isPlaying = true;
        audio.PlayOneShot(noAmmo, 1.0f);
        yield return new WaitForSeconds(2);
        _isPlaying = false;
    }
   



}
