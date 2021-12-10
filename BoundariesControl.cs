using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundariesControl : MonoBehaviour
{
    public GameObject KillerPref;
    public bool killerSpawned = false;
    public Vector2 xRange = new Vector2(-1000,1000);
    public Vector2 yRange = new Vector2(-1000, 1000);
    public Vector2 zRange = new Vector2(-1000, 1000);
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject!=null)
        {
            
            Vector3 pos = player.transform.position;
            if ((pos.x<xRange.x||pos.x>xRange.y|| pos.y < yRange.x || pos.y > yRange.y||pos.z<zRange.x||pos.z>zRange.y)&&!killerSpawned) {
                GameObject newEnemy = Instantiate(KillerPref, transform.position,transform.rotation);
                newEnemy.GetComponent<CrascEnemyAI>().currentTarget = player.transform;
                newEnemy.GetComponent<CrascEnemyAI>().path = new List<Transform>(1);
                newEnemy.GetComponent<CrascEnemyAI>().path.Add(player.transform);
                newEnemy.GetComponent<CrascEnemyAI>().patrolSpeed = 50;
                newEnemy.GetComponent<CrascEnemyAI>().instantKill = true;
                killerSpawned = true;
                Debug.Log("Left boundaries");
            }
        } else
        {
            Debug.Log("Lies");
        }
    }
}
