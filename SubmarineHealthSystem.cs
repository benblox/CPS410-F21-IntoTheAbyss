using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SubmarineHealthSystem : MonoBehaviour
{
    public AudioSource metalClank;
    public AudioSource subAlarm;
    public AudioClip warningVoice;
    public AudioSource submarine;
    public int health = 5;

    private float collisionSleepTimerFloat = 0f;
    private int collisionSleepTimer = 0;

    public Animator fadeOutController;

    public GameObject criticalViewImage;

    void OnCollisionEnter(Collision collision)
    {

       if (collision.collider.gameObject.tag == "Enemy")                    //makes sure it collides with an enemy
       {
            Debug.Log("here");

            if(!metalClank.isPlaying)                                                                                           //helps the delay
            {
                health--;

                metalClank.Play();

          
                if(health <= 2)                                                                                                // goes into "critcal mode"
                {
                    subAlarm.loop = true;                                                                                       
                    subAlarm.Play();
                    StartCoroutine(LowHealth());    
                    criticalViewImage.SetActive(true);                                                                          //enables the red view

                }

                if (health == 0)
                {

                    fadeOutController.Play("FadeOutLevel", 0);

                    while (1 == 1)                                                                                              //a loop to delay the code until the fade out is done playing
                    {
                        collisionSleepTimerFloat += Time.deltaTime;                                                             

                        collisionSleepTimer = (int)(collisionSleepTimerFloat % 60);                                             //delays for 3 seconds

                        if (collisionSleepTimer == 3)                                                                           //delays for 3 seconds
                        {
                            collisionSleepTimerFloat = 0f;
                            collisionSleepTimer = 0;
                            break;
                        }
                    }
                     
                    SceneManager.LoadScene("MainMenu");
                    health = 5;                                                                                                 //reset health when reloading
                    criticalViewImage.SetActive(false);

                }
                    
            }


            while (1 == 1)                                                                                                       //delay any collisions by 3 seconds
            {
                collisionSleepTimerFloat += Time.deltaTime;

                collisionSleepTimer = (int)(collisionSleepTimerFloat % 60);

                if (collisionSleepTimer == 3)
                {
                    break;
                }
                
            }

            collisionSleepTimerFloat = 0f;
            collisionSleepTimer = 0;

            //while()

            Debug.Log("health + " + health);
       }
    }
    IEnumerator LowHealth()
    {
        yield return new WaitForSeconds(2);
        submarine.PlayOneShot(warningVoice);
    }
}
