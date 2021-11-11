using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class destroyedSubScript : MonoBehaviour
{
    private AudioSource audioSelf;
    public AudioClip[] blackBox;
    private AudioSource submarine;
    private bool collected = false;
    public bool _hasNextLine;
    public GameObject nextLine;

    void Start()
    {
        audioSelf = GetComponent<AudioSource>();
        audioSelf.maxDistance = GetComponent<SphereCollider>().radius;
    }

    //PLAYER ENTERS DISTRESS RANGE, PLAY AUDIO
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerSub")
        {
            print("Player entered");
            audioSelf.Play();
            GameObject.FindGameObjectWithTag("DistTracker").GetComponent<DistanceToTarget>().updateTarget(transform.gameObject);
        }
        
    }
    //PLAYER LEAVES DISTRESS RANGE, STOP AUDIO
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerSub")
        {
            print("Player left");
            audioSelf.Stop();
            GameObject.FindGameObjectWithTag("DistTracker").GetComponent<DistanceToTarget>().loseTarget();
        }
    }

    //PLAYER TOUCHES SUB, GETS AUDIO + RESOURCES
    public void pickedUp()
    {
        collected = true;
        audioSelf.Stop();
        GameObject.FindGameObjectWithTag("DistTracker").GetComponent<DistanceToTarget>().loseTarget();
        //if (SceneManager.GetActiveScene().buildIndex == 2)
        //{
        //    GameObject.FindGameObjectWithTag("Objective").GetComponent<stageOne>().foundSub();
        //}
        //transform.GetChild(0).gameObject.SetActive(false);
        //submarine = collision.transform.GetComponent<AudioSource>();
        //StartCoroutine(BlackBox());
    }

    IEnumerator BlackBox()
    {
        //collected = true;
        submarine.PlayOneShot(blackBox[0]);
        yield return new WaitForSeconds(blackBox[0].length + 2);
        
        submarine.PlayOneShot(blackBox[1]);
        yield return new WaitForSeconds(blackBox[1].length);

        if(_hasNextLine == true)
        {
            nextLine.SetActive(true);
        }
          
    }
}
