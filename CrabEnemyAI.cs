using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrabEnemyAI : MonoBehaviour
{
    public Transform goal;
    NavMeshAgent agent;
    Rigidbody rb;
    public bool jumped = false;
    Vector3 startPosition;
    Vector3 distanceFromTarget;
    public string state = "Attack";
    public bool canAttach = true;
    public float attackRange;
    public float pull = 1f;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (state == "Attack")
        {
            if ((goal.transform.position - startPosition).magnitude < attackRange)
            {
                if (agent.enabled)
                {
                    RaycastHit hit;
                    Physics.Raycast(new Ray(goal.transform.position, Vector3.up * -1f), out hit, Mathf.Infinity);
                    agent.destination = hit.point;

                    //    agent.destination = goal.position;

                }
                distanceFromTarget = (goal.position - transform.position);
                distanceFromTarget = new Vector3(distanceFromTarget.x, 0f, distanceFromTarget.z);
                if (distanceFromTarget.magnitude < 10f && !jumped)
                {
                    agent.enabled = false;

                    rb.velocity = Vector3.up * 9f + distanceFromTarget.normalized * 10f;

                    rb.useGravity = true;
                    jumped = true;
                }
            }
            else
            {
                if (agent.enabled)
                    agent.destination = startPosition;
            }

        }
        else if (state == "Attached")
        {
            
            if (Input.GetKeyDown(KeyCode.CapsLock))
            {
                Debug.Log("Detached");
                Detach();
            }
            goal.GetComponent<Rigidbody>().AddForce(Vector3.down*pull);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            
            while (!Physics.Raycast(new Ray(transform.position - Vector3.up * 0.5f, Vector3.up * -1f), 0.5f))
            {
                transform.position += Vector3.up * 0.1f;
            }
            agent.enabled = true;
            agent.ResetPath();
            rb.velocity = Vector3.zero;
            jumped = false;
            canAttach = true;
        }
        else if (collision.gameObject == goal.gameObject && canAttach)
        {
            jumped = true;
            agent.enabled = false;
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;
            state = "Attached";
            canAttach = false;
            transform.parent = goal.transform;
            
            
        }
    }
    public void Detach()
    {
        
        rb.useGravity = true;
        rb.isKinematic = false;
        state = "Attack";
        transform.parent = null;
    }
}

