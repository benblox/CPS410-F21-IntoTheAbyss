using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Place this script on a GameObject with an isTrigger collider.
//This script will force the sub to lose it's current objective.
public class objectiveDropper : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerSub")
            GameObject.FindGameObjectWithTag("DistTracker").GetComponent<DistanceToTarget>().loseTarget();
    }
}
