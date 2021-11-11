using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class stageOne : MonoBehaviour
{
    public GameObject[] subs;
    private GameObject player;
    public GameObject barrier;
    public GameObject toNextScene;

    private int subsFounds = 0;
    private int numSubs;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerSub");
        numSubs = subs.Length;

        barrier.SetActive(true);
        toNextScene.SetActive(false);
    }

    public void foundSub()
    {
        subsFounds++;
        checkProgress();
    }

    private void checkProgress()
    {
        if (subsFounds >= numSubs)
        {
            barrier.SetActive(false);
            toNextScene.SetActive(true);
            print("level complete: opening path to next level");
            GameObject.FindGameObjectWithTag("DistTracker").GetComponent<DistanceToTarget>().updateTarget(toNextScene);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerSub")
        {
            //turn off sub
            player.GetComponent<submarine>().disableMovement();

            //look at cave

            //fade out?

            //load next scene
            StartCoroutine(wait());
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
