using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class tutorialHandler : MonoBehaviour
{
    public AudioClip clip;
    public AudioClip craneMotorSound;
    public AudioClip craneDrop;
    public GameObject[] Gates;

    //tutorial voicelines
    public AudioClip[] tutVA;

    public GameObject playerSub;
    private submarine subScript;
    private DistanceToTarget obj_tracker;
    private AudioSource subSound;
    private Rigidbody subRB;
    private AudioSource craneMotor;
    private Animator anim;
    private Animator subAnim;
    private Player p1;
    private Player p2;

    // Build references to components
    void Start()
    {
        anim = GetComponent<Animator>();
        //playerSub = GameObject.FindGameObjectWithTag("PlayerSub");
        obj_tracker = GameObject.FindGameObjectWithTag("DistTracker").GetComponent<DistanceToTarget>();
        subRB = playerSub.GetComponent<Rigidbody>();
        subScript = playerSub.GetComponent<submarine>();
        subSound = playerSub.GetComponent<AudioSource>();
        craneMotor = GetComponentInChildren<AudioSource>();
        subAnim = playerSub.GetComponent<Animator>();
        p1 = ReInput.players.GetPlayer(0);

        //if tutorial is disabled, open gates and disable cutscene
        if (!subScript.isTutorial)
        {
            anim.enabled = false;
            Gates[0].GetComponent<gate>().openGate();
            Gates[1].GetComponent<gate>().openGate();
            Gates[2].GetComponent<gate>().openGate();
            Gates[3].GetComponent<gate>().openGate();
        }
    }

    //called at start of scene by animation event.
    //disables player controls and queues up sound clips
    public void turnOffSub()
    {
        canProgress = false;
        StartCoroutine(waitToPlay(0.6f, subSound, clip));
        //subSound.clip = clip;
        
        //subSound.PlayDelayed(0.55f);
        StartCoroutine(waitToPlay(1f, craneMotor, craneMotorSound));
        StartCoroutine(waitToPlay(17.2f, craneMotor, craneDrop));
        obj_tracker.setObjective("-------");
    }

    //called once the sub is dropped in the water by an animation event.
    //returns controls to player and starts tutorial
    public void turnOnSub()
    {
        subAnim.enabled = false;
        enableConstraints();
        StartCoroutine(advanceTutorial("Movement"));
    }


    //======== TUTORIAL =========
    public int progress = 0;
    private bool canProgress = false;
    private bool boosted = false;
    private bool flared = false;


    //Listens for inputs based on tutorial progress.
    //Once input is detected, advance the tutorial.
    private void Update()
    {
        if (canProgress)
        {
            //Listens for inputs
            switch (progress)
            {
                // == FIRST AREA ==
                //Move the sub forward
                case 0:
                    if (p1.GetAxis("Thrust") > 0)
                    {
                        canProgress = false;
                        progress++;
                        StartCoroutine(advanceTutorial());
                    }
                    break;
                //Not bad, vitals still on
                case 1:
                    canProgress = false;
                    progress++;
                    StartCoroutine(advanceTutorial("Turning"));
                    break;
                //Turn the sub
                case 2:
                    if (p1.GetAxis("Pitch") != 0 || p1.GetAxis("Yaw") != 0)
                    {
                        canProgress = false;
                        progress++;
                        StartCoroutine(advanceTutorial());
                    }
                    break;
                //don't lose lunch, more water up ahead
                case 3:
                    canProgress = false;
                    progress++;
                    Gates[0].GetComponent<gate>().openGate();
                    obj_tracker.updateTarget(Gates[0]);
                    break;
                // == SECOND AREA ==
                //turn on lights
                case 4:
                    if (p1.GetButtonDown("Flashlights"))
                    {
                        canProgress = false;
                        progress++;
                        StartCoroutine(advanceTutorial("Depth"));
                    }
                    break;
                //increase depth
                case 5:
                    if (p1.GetAxis("Float") < 0)
                    {
                        canProgress = false;
                        progress++;
                        StartCoroutine(advanceTutorial());
                    }
                    break;
                //popcorn in head
                case 6:
                    canProgress = false;
                    progress++;
                    Gates[1].GetComponent<gate>().openGate();
                    obj_tracker.updateTarget(Gates[1]);
                    break;
                // == LAST AREA ==
                //use boost + flare
                case 7:
                    if (p1.GetButtonDown("Flare")) flared = true;
                    if (p1.GetButtonDown("Boost")) boosted = true;
                    if (flared && boosted)
                    {
                        canProgress = false;
                        progress++;
                        StartCoroutine(advanceTutorial());
                    }
                    break;
                //kidding about critters
                case 8:
                    canProgress = false;
                    progress++;
                    Gates[2].GetComponent<gate>().openGate();
                    obj_tracker.updateTarget(Gates[2]);
                    break;
                //last VA line
                case 9:
                    canProgress = false;
                    Gates[3].GetComponent<gate>().openGate();
                    obj_tracker.updateTarget(Gates[3]);
                    break;
                default:
                    break;
            }
        }
    }

    //Plays the next voiceline and enables the next action/ability
    IEnumerator advanceTutorial(string methodName = "")
    {
        subSound.PlayOneShot(tutVA[progress]);
        yield return new WaitForSeconds(tutVA[progress].length);
        
        //Todo:
        //change methodName to progress.
        //enable all objective texts here
        switch (methodName)
        {
            case "Movement":
                subScript.enableMovement();
                obj_tracker.setObjective("Move Forward");
                break;
            case "Turning":
                disableConstraints();
                obj_tracker.setObjective("Turn Submarine");
                subScript.enableTurning();
                break;
            case "Flare":
                subScript.enableFlare();
                break;
            case "Boost":
                subScript.enableBoost();
                obj_tracker.setObjective("Use Flare & Boost");
                break;
            case "Light":
                subScript.enableLight();
                obj_tracker.setObjective("Use Floodlights");
                break;
            case "Depth":
                obj_tracker.setObjective("Increase depth");
                break;
            default:
                break;
        }

        canProgress = true;
    }

    //helper function for playing audio after a delay
    IEnumerator waitToPlay(float seconds, AudioSource source, AudioClip sound)
    {
        yield return new WaitForSeconds(seconds);
        source.PlayOneShot(sound);
    }


    //Constricts the subs movement so it can only move forward
    private void enableConstraints()
    {
        subRB.constraints 
            = RigidbodyConstraints.FreezeRotation
            | RigidbodyConstraints.FreezePositionY;
        //= RigidbodyConstraints.FreezeRotation 
        subScript.enableTurning();
        subScript.disableTurning();

    }
    private void disableConstraints()
    {
        subRB.constraints = RigidbodyConstraints.None;
    }

    //Called by the gates in the tutorial scene.
    //Progresses tutorial once player enters through the gate.
    public void gateAdvance(int gateNo)
    {
        if (gateNo == 1) StartCoroutine(advanceTutorial("Light"));
        else if (gateNo == 2)
        {
            StartCoroutine(advanceTutorial("Boost"));
            subScript.enableFlare();
        }
        else if (gateNo == 3) StartCoroutine(advanceTutorial());
    }

    public void turnOffGaiaAudio()
    {
        GameObject ga = GameObject.Find("Gaia Audio");
        ga.SetActive(false);
    }
}
