using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class VAScript : MonoBehaviour
{


    /*
     *  VAScript
     *  Used to play different voice lines during game via collision with game object
     * 
     * Variables:
     * lines: list of audioclips (if more than 1)
     * subaudio: audiosource in submarine
     */
    private List<AudioClip> lines = new List<AudioClip>();
    private AudioSource subAudio;

    // Start is called before the first frame update

    // Update is called once per frame

    /*
     * VoiceLines
     * used to play sound lines in game
     * uses an array if there is more than 1 voice line for onTriggerEnter
     */
    IEnumerator VoiceLines()
    {

        for (int i = 0; i < lines.Count; i++)
        {
            if (subAudio.isPlaying)
            {
                subAudio.Stop();
            }
            subAudio.PlayOneShot(lines[i]);
            yield return new WaitForSeconds(lines[i].length + 1);
            lines.Clear();
        }
        //textLine.text = "";
    }


    /*
     * OnTriggerEnter
     * if player sub hits voiceacting empties
     */
    private void OnTriggerEnter(Collider other)
    {
        //print(other);
        if (other.transform.tag == "PlayerSub")
        {
            if (gameObject.tag == "DestroyedSub")
            {
                switch (MasterSingleton.Instance.easterEggSubIndex)
                {
                    
                    case 0:
                    {
                            lines.Add(Resources.Load("Audio/VA/Optionals/Submarine01-Henson") as AudioClip);
                            MasterSingleton.Instance.easterEggSubIndex++;
                            break;
                    }

                    case 1:
                    {
                            lines.Add(Resources.Load("Audio/VA/Optionals/Submarine02-Biologists") as AudioClip);
                            MasterSingleton.Instance.easterEggSubIndex++;
                            break;
                    }
                }
            }
            else if (gameObject.tag == "OptionalVoiceTrigger")
            {
                switch (MasterSingleton.Instance.optionalLineIndex)
                {

                    case 0:
                    {
                            lines.Add(Resources.Load("Audio/VA/Boat/Act2Scene2") as AudioClip);
                            MasterSingleton.Instance.optionalLineIndex++;
                            break;
                    }

                    case 1:
                    {
                            lines.Add(Resources.Load("Audio/VA/Boat/Act2Scene3") as AudioClip);
                            MasterSingleton.Instance.optionalLineIndex++;
                            break;
                    }
                    case 2:
                    {
                            lines.Add(Resources.Load("Audio/VA/Boat/Act3Scene2") as AudioClip);
                            MasterSingleton.Instance.optionalLineIndex++;
                            break;
                    }

                }
            }
            else if (gameObject.tag == "VoiceTrigger")
            { 

                switch (MasterSingleton.Instance.basicLineIndex)
                {

                    case 0:
                    {
                            Debug.Log("run?");
                            lines.Add(Resources.Load("Audio/VA/Boat/Act1Scene1FilteredFinal") as AudioClip);
                            MasterSingleton.Instance.basicLineIndex++;
                            break;
                    }

                    case 1:
                    {
                            Debug.Log("not broken");
                            lines.Add(Resources.Load("Audio/VA/Boat/Act2Scene1") as AudioClip);
                            MasterSingleton.Instance.basicLineIndex++;
                            break;
                    }

                    case 2:
                    {
                            lines.Add(Resources.Load("Audio/VA/Boat/Act3Scene1") as AudioClip);
                            MasterSingleton.Instance.basicLineIndex++;
                            break;
                    }
                    case 3:
                    {
                            lines.Add(Resources.Load("Audio/VA/Boat/Act3Scene3") as AudioClip);
                            MasterSingleton.Instance.basicLineIndex++;
                            break;
                    }
                    case 4:
                    {
                            lines.Add(Resources.Load("Audio/VA/Boat/Act3Scene4") as AudioClip);
                            MasterSingleton.Instance.basicLineIndex++;
                            break;
                    }
                    case 5:
                    {
                            lines.Add(Resources.Load("Audio/VA/Boat/Act4Scene1Filtered") as AudioClip);
                            MasterSingleton.Instance.basicLineIndex++;
                            break;
                    }
                }
            }
            if (lines[0] == null)
            {
                Debug.Log("broken");
            }
            GetComponent<BoxCollider>().enabled = false;
            subAudio = other.transform.GetComponent<AudioSource>();
            StartCoroutine(VoiceLines());
            
        }
    }


}
