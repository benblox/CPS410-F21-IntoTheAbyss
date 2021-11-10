using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Spawn Monster
 * used to spawn monster on collision with empty
 * Vairables: 
 * monster: monster gameobject
 */
public class SpawnMonster : MonoBehaviour
{
    public GameObject monster;
    // Start is called before the first frame update
   

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("PlayerSub"))
        {
            StartCoroutine(monsterSpawn());
        }
    }
    
    IEnumerator monsterSpawn()
    {
        //waits 8 seconds, then spawns monster
        yield return new WaitForSeconds(8);
        monster.SetActive(true);
    }
}
