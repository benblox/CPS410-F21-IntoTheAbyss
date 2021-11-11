using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate : MonoBehaviour
{
    public int gateNo = 0;
    private Animator anim;
    private AudioSource sound;
    private tutorialHandler th;
    private bool activated = false;
    private endLevel endLevelscript;

    void Awake()
    {
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        th = FindObjectOfType<tutorialHandler>();
        endLevelscript = GetComponent<endLevel>();
    }

    public void openGate()
    {
        anim.Play("Open");
        sound.pitch = Random.Range(0.92f, 1.08f);
        sound.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerSub" && !activated){
            if (gateNo != 4)
            {
                activated = true;
                th.gateAdvance(gateNo);
            }
            //This takes the player to the next scene
            else
            {
                endLevelscript.lookAtPoint();
            }
        }
    }
}
