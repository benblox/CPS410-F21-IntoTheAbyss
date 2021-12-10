using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEnemyAI : MonoBehaviour
{
    public GameObject target = null;
    public GameObject player;
    public Rigidbody rb;
    public string state = "Await";
    public float sightRange = 50;
    public Transform origin;
    public float aggression = 0;
    public float maxAggression;
    public float attackCooldown;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        if (origin!=null)
        {
            transform.position = origin.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case "Await":
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                if (FlareDistract.getNearestFlare(transform, sightRange) != null)
                {
                    state = "Distract";
                    target = FlareDistract.getNearestFlare(transform, sightRange).gameObject;
                } else if (Vector3.Distance(transform.position, player.transform.position) < sightRange) { 
                    aggression += Time.deltaTime;
                    if (aggression>=maxAggression)
                    {
                        aggression = attackCooldown;
                        state = "Attack";
                    }
                } else
                {
                    if (Vector3.Distance(origin.position,transform.position)>3)
                    {
                        transform.LookAt(origin, Vector3.up);
                        rb.velocity = transform.forward * 5;
                    } else
                    {
                        rb.velocity = Vector3.zero;
                    }
                    
                }
                break;
            case "Attack":
                if (target == null)
                {
                    FlareDistract newFlare = FlareDistract.getNearestFlare(transform);
                    if (newFlare != null)
                    {
                        target = newFlare.gameObject;
                    }
                    else
                    {
                        target = player;
                    }
                    
                }
                else if (target == player)
                {
                    FlareDistract newFlare = FlareDistract.getNearestFlare(transform);
                    if (newFlare != null)
                    {
                        target = newFlare.gameObject;
                    }
                }
                transform.LookAt(target.transform,Vector3.up);
                rb.velocity = transform.forward * 5;
                aggression -= Time.deltaTime;
                if (aggression <= 0)
                {
                    state = "Await";
                }
                break;
            case "Distract":
                if (target==null)
                {
                    FlareDistract newFlare = FlareDistract.getNearestFlare(transform);
                    if (newFlare != null)
                    {
                        target = newFlare.gameObject;
                    } else
                    {
                        state = "Await";
                        break;
                    }
                }
                transform.LookAt(target.transform, Vector3.up);
                rb.velocity = transform.forward * 5;
                break;
        }
        
        

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<SubmarineHealthSystem>()!=null)
        {
            collision.gameObject.GetComponent<SubmarineHealthSystem>().health -= 1;
            aggression = 0f;
            state = "Await";
        }
    }
}
