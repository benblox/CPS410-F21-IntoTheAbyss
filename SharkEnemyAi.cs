using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SharkEnemyAi : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public List<Transform> path = new List<Transform>();
    int indexOfTarget;
    public Transform currentTarget = null;
    public Transform Player;
    public float attackrange = 50;
    public float returnrange = 50;
    public string state = "Patrol";

    private Animator anim;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("PlayerSub").transform;
        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();
        if (path.Count>0)
        {
            currentTarget = path[0];
            indexOfTarget = 0;
        }
    }

    // CVB 12/3/21 - Changed this to fixed update from update. 
    // fixed update should be used for rigidbodies / physics calculations
    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
    void FixedUpdate()
    {
        switch (state)
        {
            case "Patrol":
                if (currentTarget != null)
                {
                    transform.LookAt(currentTarget);
                    rb.velocity = transform.forward * 5f;
                    if (Vector3.Distance(transform.position, currentTarget.transform.position) < 2f)
                    {
                        indexOfTarget = indexOfTarget == path.Count - 1 ? 0 : indexOfTarget + 1;
                        currentTarget = path[indexOfTarget];
                    }
                    
                    if (Physics.Raycast(new Ray(transform.position,Player.position-transform.position),attackrange))
                    {
                        state = "Attack";
                    }
                }
                break;
            case "Attack":
                StartCoroutine("ChargeDecide");

                break;
            case "Return":
                transform.LookAt(currentTarget);
                rb.velocity = transform.forward * 8f;
                if (Vector3.Distance(transform.position, currentTarget.transform.position) < 5f)
                {
                    indexOfTarget = indexOfTarget == path.Count - 1 ? 0 : indexOfTarget + 1;
                    currentTarget = path[indexOfTarget];
                    state = "Patrol";
                }
                break;

        }


    }
    IEnumerator ChargeDecide()
    {
        bool shouldreturn = true;
        int currentindex = 0;
        while (shouldreturn == true && currentindex < path.Count)
        {
            if (Vector3.Distance(path[currentindex].position, transform.position) < returnrange)
            {
                shouldreturn = false;
            }
            currentindex += 1;
        }

        if (shouldreturn)
        {
            state = "Return";
            currentTarget = path[0];
            indexOfTarget = 0;
            yield return null;
        } else
        {
            transform.LookAt(Player);
            anim.Play("PA_SharkBiteClip");
            rb.velocity = transform.forward * 20f;
            state = "Charge";

            yield return new WaitForSeconds(10f);
            StartCoroutine("ChargeDecide");
        }
        yield return null;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerSub"))
        {
            if (Input.GetKey(KeyCode.CapsLock))
            {
                StopCoroutine("ChargeDecide");
                anim.Play("PA_SharkKnockBackClip");
                //state = "Return";
                rb.AddForce((transform.position - collision.transform.position).normalized * 200f);
                StartCoroutine("Reawaken");
                // rb.velocity = (Collision.transform.position - transform.position).normalized * 50f;
            } else
            {
                collision.gameObject.GetComponentInChildren<SubmarineHealthSystem>().health -= 1;
                collision.gameObject.GetComponentInChildren<Rigidbody>().AddForce((collision.gameObject.transform.position - transform.position).normalized * 500f);
                rb.velocity = rb.velocity.normalized;
            }
            
        }
    }
    IEnumerator Reawaken()
    {
        //Return speed to 1 after stun time finishes
        //this returns the animator speed back to normal
        yield return new WaitForSeconds(30f);

        state = "Attack";
        yield return null;
    }
}
