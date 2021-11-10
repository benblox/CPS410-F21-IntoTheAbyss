using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debug_player_mover : MonoBehaviour
{
    public bool enable = false;
    public int playerLocation = 0;
    public GameObject[] locations;
    private GameObject player;
    public bool activate = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("PlayerSub");
    }

    // Update is called once per frame
    void Start()
    {
        if (enable)
        {
            movePlayer();
        }
    }
    private void Update()
    {
        if (activate)
        {
            movePlayer();
            activate = false;
        }
    }

    private void movePlayer()
    {
        player.transform.position = locations[playerLocation].transform.position;
        player.transform.rotation = locations[playerLocation].transform.rotation;
    }
}
