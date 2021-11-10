/**
 * Project: Into the Abyss
 * Script: CrascAI
 * Script Author: Colby Schaeding
 * 
 * Description: This script handles the movement, attacks, and
 *              animations related to the Crasc enemy [the one
 *              that looks like a giant pancake]. After the
 *              player locates and retrieves the first black
 *              box in level 1, Crasc will set its 'target'
 *              variable as the player's submarine and slowly
 *              move towards them. 
 * 
 */
 

 /**
  * TO-DO:
  * 
  * -Get basic 'target' and movement functions working
  * -Change 'target' to the Flare object when one is shot
  * 
  * 
  * IDEAS:
  * -If speed is below a certain threshold + the sub's lights are OFF,
  *     Crasc goes away for a bit?
  *     
  * -Light source on Crasc that looks like an objective. When sub enters
  *     a certain range, have Crasc emerge from the ground + attack?
  *         -Spawn sand particles when emerging.
  *         -May need custom animation(s)
  **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrascAI : MonoBehaviour
{

    // initialize variables
    private Transform target;
    private float speed;
    private int targetCooldown;

    private bool debug = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("PlayerSub").transform;
        targetCooldown = 7;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * This method will update the Crasc's current animation.
     * A work-in-progress/placeholder for now
     */
    private void UpdateAnimation ()
    {
        
    }

    /*
     * An OnTriggerEnter method borrowed from the
     * old AI Script. Used to change Crasc's target
     * from the PlayerSub to a Flare shot from the sub.
     */
    void OnTriggerEnter(Collider col)
    {
        if (debug) print("OnEnter: " + col.gameObject.tag);

        //update chase target
        if (col.gameObject.CompareTag("flare"))
        {
            changeTarget(col.transform);
            //col.transform.GetComponent<flarebullet>().beingTargeted(transform.gameObject);

            StartCoroutine(SwitchTargetCooldown());
        }

        else if (col.gameObject.name == "outer windscreen")
            changeTarget(col.transform);
    }



    /*
     * This method will allow the Crasc to temporarily
     * switch its target to a Flare when shot. Method
     * is public so it may be called from other scripts.
     * Target change will be on a timer using IEnumerator.
     * 
     * If PlayerSub is outside a certain distance, temporarily
     * despawn Crasc?
     */
     public void SwitchTarget()
    {
        //target = GameObject.Find("Flare");
    }

    /*
     * IEnumerator function that will switch targets
     * back to the PlayerSub after a short period of time.
     */
    IEnumerator SwitchTargetCooldown()
    {
        yield return new WaitForSeconds(targetCooldown);
    }



    /*
     * Two more methods borrowed from the old AI script.
     * Currently needed for the OnTriggerEnter function.
     * 
     */
    private void changeTarget(Transform newTarget)
    {
        target = newTarget;
        if (debug) print("switching targets!");
    }
    public void loseTarget()
    {
        target = null;
        if (debug) print("Lost target!");
    }

}
