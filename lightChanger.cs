using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightChanger : MonoBehaviour
{
    public GameObject sub;
    public GameObject start;
    public GameObject end;
    public Light dirLight;
    private float subXPos;
    private float lightStartIntensity;
    private float lightEndIntensity = 0.8f;
    private float lightRange;
    private float startX;
    private float endX;
    private float posRange;
    // Start is called before the first frame update
    void Start()
    {
        lightStartIntensity = dirLight.intensity;
        lightRange = lightStartIntensity - lightEndIntensity;

        startX = Mathf.Abs(start.transform.position.x);
        endX = Mathf.Abs(end.transform.position.x);
        posRange = endX - startX;
        //print(startX + ", " + endX);
    }

    // Update is called once per frame
    void Update()
    {
        subXPos = Mathf.Abs(sub.transform.position.x);

        if (subXPos < endX && subXPos > startX)
        {
            float percent = (subXPos - startX) / posRange;
            //print("Percent through effect: " + (subXPos - startX) / posRange);
            dirLight.intensity = lightEndIntensity + lightRange * (1f - percent);
        }
    }
}
