using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrascEnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public List<Transform> path = new List<Transform>();
    public bool instantKill = false;
    public bool canHit = true;
    public Transform currentTarget = null;
    public Transform startPosition;
    public string state;
    public Transform Player;
    public float patrolSpeed = 5f;
    public int indexOfTarget = 0;
    void Start()
    {
        state = "Patrol";
        rb = GetComponent<Rigidbody>();
        if (path.Count>0)
        {
            currentTarget = path[0];
            indexOfTarget = 0;
        }
        if (startPosition!=null)
        {
            transform.position = startPosition.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case "Patrol":
                if (currentTarget != null)
                {
                    transform.LookAt(currentTarget);
                    rb.velocity = transform.forward * patrolSpeed;
                    if (Vector3.Distance(transform.position, currentTarget.transform.position) < 2f)
                    {
                        if (indexOfTarget == path.Count - 1)
                        {
                            if (instantKill)
                            {
                                state = "Wait";
                            } else
                            {
                                indexOfTarget = 0;
                                currentTarget = path[indexOfTarget];
                            }
                            
                        } else
                        {
                            indexOfTarget += 1;
                            currentTarget = path[indexOfTarget];
                        }
                        
                    }
                }
                break;
            case "Wait":
                transform.position = currentTarget.transform.position;
                rb.velocity = Vector3.zero;
                break;

        }


    
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerSub"))
        {
            if (instantKill)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (canHit)
            {
// collision.gameObject.GetComponentInChildren<SubmarineHealthSystem>().health -= 1;
                Debug.Log(true);
                canHit = false;
                StartCoroutine("resetHit");
            }
            
        }
    }
    IEnumerator resetHit()
    {
        yield return new WaitForSeconds(3f);
        canHit = true;
        yield return null;
    }
}
