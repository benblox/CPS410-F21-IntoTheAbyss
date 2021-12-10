using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSingleton : MonoBehaviour
{
    private static MasterSingleton _instance;

    public int basicLineIndex = 0;
    public int optionalLineIndex = 0;
    public int easterEggSubIndex = 0;
    public bool plant1Activated = false;
    public bool plant2Activated = false;
    

    public static MasterSingleton Instance 
    {   
        get { return _instance; } 
    }
    

    private void Awake() 
    { 
        if (_instance != null && _instance != this) 
        { 
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
       

}
