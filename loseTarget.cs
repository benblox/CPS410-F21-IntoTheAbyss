using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loseTarget : MonoBehaviour
{
    private bool activated = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerSub")
        {
            activated = true;
            GameObject.FindGameObjectWithTag("DistTracker").GetComponent<DistanceToTarget>().loseTarget();
        }
    }
}
