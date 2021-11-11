using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flareAmmoPickup : MonoBehaviour
{
    private AudioSource sound;
    private bool collected = false;
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        sound.pitch = Random.Range(0.8f, 1.2f);
        sound.Play();
        if (collision.gameObject.tag == "PlayerSub")
        {
            collision.transform.GetComponent<SubFlare>().getAmmo(Random.Range(3, 6));
            Destroy(this);
        }
    }
}
