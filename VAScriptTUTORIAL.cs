using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * VAScriptTutorial
 * this script is used for very specific commands in tutorial
 * Variables:
 * lines: list of audioclips (if more than 1)
 * subaudio: audiosource in submarine
 * _isPlaying: checks to see if sound is playing
 * _usedBoost: used to check if player has used boost
 * _usedFlare: used to check if player has used flare
 * i: check what stage of tutorial is on.
 */
public class VAScriptTUTORIAL : MonoBehaviour
{
    public AudioClip[] lines;
    public AudioSource subAudio;
  
    private bool _isPlaying;
    private bool _usedBoost;
    private bool _usedFlare;
    private int i;
    //public BoxCollider trigger;


   
    // Start is called before the first frame update
    void Start()
    {
        
        _isPlaying = false;
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //checks for increase depth
        if(i == 0)
        {
            if (Input.GetKey(KeyCode.LeftControl) && !_isPlaying)
            {
                StartCoroutine(VoiceLines());
                    i++;
     
            }

        }

        //checks for moving left or right
        else if(i == 1)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) && !_isPlaying)
            {

                    StartCoroutine(VoiceLines());
                    i++;
            }
        }
        //checks for boost and flare
        else if( i == 2)
        {
            if(Input.GetKeyDown(KeyCode.CapsLock))
            {
                _usedBoost = true;
            }
            if(Input.GetKeyDown(KeyCode.P))
            {
                _usedFlare = true;
            }
            if(_usedBoost && _usedFlare && !_isPlaying)
            {
                StartCoroutine(VoiceLines());
                i++;
            }
        }
        //checks for lights
        else if (i == 3)
        {
            if(Input.GetKeyDown(KeyCode.F))
                {
                StartCoroutine(VoiceLines());
                i++;
            }
        }
    }

    /*
     * voiceLines
     * plays audio when needed
     * 
     */
    IEnumerator VoiceLines()
    {
        _isPlaying = true;
        if (subAudio.isPlaying)
        {
            subAudio.Stop();
        }
        subAudio.PlayOneShot(lines[i]);
        yield return new WaitForSeconds(lines[i].length);
        _isPlaying = false;
        //Destroy(transform.parent.gameObject);
        
    }

   
    private void OnTriggerEnter(Collider other)
    {
        //print(other);
        //if (other.transform.CompareTag("trigger"))
        {
            //other.GetComponent<BoxCollider>().enabled = false;
            
            //StartCoroutine(VoiceLines());
           

        }
    }
}


