using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Transparent : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject plane;
    public GameObject planeTwo;

    void Start()
    {
        plane.GetComponent<Renderer>().enabled = false;
        planeTwo.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
