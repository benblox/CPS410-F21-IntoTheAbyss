using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script gets attached to the gameobject that has the "pickup" collider on the destroyed sub.
//It just tells the destroyed sub script that it has been picked up.
public class destroyedSubHelper : MonoBehaviour
{
    private bool collected = false;
    private destroyedSubScript dss;

    private void Start()
    {
        dss = transform.parent.GetComponent<destroyedSubScript>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerSub" && !collected)
        {
            collected = true;
            dss.pickedUp();
        }
    }
}
