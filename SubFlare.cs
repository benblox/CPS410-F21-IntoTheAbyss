using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubFlare : MonoBehaviour
{
    public GameObject flare;
    public Transform gun;
    public int ammo;
    public int cooldown = 4;
    public int shotPower = 25;
    public GameObject indicatorLight;
    public TextMeshProUGUI numFlares;
    private bool canShoot = true;

    private void Start()
    {
        numFlares.text = ammo.ToString();
    }
    public void shootFlare()
    {
        if (canShoot && ammo > 0)
        {
            canShoot = false;
            GameObject shot = Instantiate(flare, gun.position, gun.rotation);
            Rigidbody rb = shot.GetComponent<Rigidbody>();
            rb.velocity = transform.TransformDirection(Vector3.forward * shotPower);
            ammo--;
            numFlares.text = ammo.ToString();
            StartCoroutine(flareCooldown());
        }
    }

    IEnumerator flareCooldown()
    {
        indicatorLight.SetActive(false);
        yield return new WaitForSeconds(cooldown);
        indicatorLight.SetActive(true);
        canShoot = true;
    }

    public void getAmmo(int rounds)
    {
        ammo += rounds;
        numFlares.text = ammo.ToString();
    }
}
