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
     * subtitles: strings with subtitles
     * textLine: text object in game
     * time: time used for subtitles
     */
    private List<AudioClip> lines = new List<AudioClip>();
    private AudioSource subAudio;
    public string[] subtitles;
    public float time;
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
        Destroy(transform.gameObject);
    }

    /*
     * Subtitles
     * controls subtitles when triggered
     * uses an array for multiple subtitles
     */

    /*
     * OnTriggerEnter
     * if player sub hits voiceacting empties, sound and subtitles start
     */
    private void OnTriggerEnter(Collider other)
    {
        //print(other);
        if (other.transform.tag == "PlayerSub")
        {   
            switch (MasterSingleton.Instance.lineIndex)
            {
                case 0:
                {
                    lines.Add(Resources.Load("Audio/VA/Boat/Act 1 Scene 1") as AudioClip);
                    lines.Add(Resources.Load("Audio/VA/Boat/Act 1 Scene 2") as AudioClip);
                    break;
                }

                case 1:
                {
                    lines.Add(Resources.Load("Audio/VA/Boat/Act 1 Scene 3 Part 1") as AudioClip);
                    lines.Add(Resources.Load("Audio/VA/Blackboxes/Submarine01-Henson") as AudioClip);
                    lines.Add(Resources.Load("Audio/VA/Boat/Act 1 Scene 3 Part 2") as AudioClip);
                    MasterSingleton.Instance.lineIndex++;
                    break;
                }

                case 2:
                {
                    lines.Add(Resources.Load("Audio/VA/Blackboxes/Submarine01-Biologists") as AudioClip);
                    lines.Add(Resources.Load("Audio/VA/Boat/Act 1 Scene 4") as AudioClip);
                    MasterSingleton.Instance.lineIndex++;
                    break;
                }
            }
            GetComponent<BoxCollider>().enabled = false;
            subAudio = other.transform.GetComponent<AudioSource>();
            StartCoroutine(VoiceLines());
            
        }
    }


}
