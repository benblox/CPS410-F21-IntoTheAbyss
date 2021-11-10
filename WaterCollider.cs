using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollider : MonoBehaviour
{
    private Transform sub;
    private Rigidbody rb;
    void Start()
    {
        sub = GameObject.FindGameObjectWithTag("PlayerSub").transform;
        rb = sub.GetComponent<Rigidbody>();
    }

    // Follow above the player
    void FixedUpdate()
    {
        transform.position = new Vector3(sub.position.x, 60f, sub.position.z);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerSub")
        {
            rb.AddForce(0f, -50f, 0f);
        }
    }
}
