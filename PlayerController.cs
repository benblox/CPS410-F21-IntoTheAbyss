using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public LevelPath levelpath;
    public float acceleration = 1;
    public float rotationspeed = 180;
    public float maxSpeed = 100;
    Quaternion targetRotation;
    private Rigidbody rb;
    private float strafe;
    private float accel;
    private float lookSide;
    private float lookUpDown;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.position = levelpath.PathPoints[1].position;
        rb.rotation = levelpath.PathPoints[1].rotation;
        targetRotation = levelpath.PathPoints[1].rotation;
        rb.velocity = Vector3.RotateTowards(rb.velocity, targetRotation * Vector3.forward, 999, 0);

    }
 
    void FixedUpdate()
    {

        Vector3 movement = new Vector3(strafe, accel, 0);
        rb.AddRelativeForce(Vector3.forward * accel * acceleration);
        rb.AddRelativeForce(Vector3.right * strafe * acceleration);

        Vector3 torque = new Vector3(-lookUpDown, lookSide, 0);
        transform.Rotate(torque.normalized * rotationspeed * Time.deltaTime);



        ////rotation around corners
        //rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRotation.normalized, rotationspeed * Time.deltaTime);
        //if (rb.position.z >= levelpath.PathPoints[currentPathPoint + 1].position.z)
        //{
        //    currentPathPoint++;
        //    targetRotation = levelpath.PathPoints[currentPathPoint].rotation;

        //    rb.velocity = Vector3.RotateTowards(rb.velocity, targetRotation * Vector3.forward, 999, 0);
        //}

    }

    public void OnMove(InputValue input)
    {
        Vector2 movementVector = input.Get<Vector2>();

        strafe = movementVector.x;
        accel = movementVector.y;
    }

    public void OnLook(InputValue input)
    {
        Vector2 movementVector = input.Get<Vector2>();

        lookSide = movementVector.x;
        lookUpDown = movementVector.y;
    }


}
