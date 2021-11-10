using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endLevel : MonoBehaviour
{
    public GameObject playerSub;
    private submarine subscript;
    private Quaternion targRotation;
    private float tweenVal = 0.25f;
    private bool shouldLook = false;
    public GameObject lookPoint;

    // Start is called before the first frame update
    void Start()
    {
        subscript = playerSub.GetComponent<submarine>();
    }

    public void lookAtPoint()
    {
        playerSub.GetComponent<submarine>().disableAll();
        targRotation = Quaternion.LookRotation(lookPoint.transform.position - playerSub.transform.position);
        StartCoroutine(fadeAndChange());
    }

    public void lookAtPoint(GameObject player)
    {
        playerSub = player;
        subscript.disableAll();
        targRotation = Quaternion.LookRotation(lookPoint.transform.position - playerSub.transform.position);
        StartCoroutine(fadeAndChange());
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLook)
        {
            playerSub.transform.rotation = Quaternion.Slerp(playerSub.transform.rotation, targRotation, tweenVal * Time.deltaTime);
            tweenVal += 0.01f;
        }
    }

    IEnumerator fadeAndChange()
    {
        shouldLook = true;
        yield return new WaitForSeconds(3f);
        //fade to black
        subscript.fade.fade2Black();
        yield return new WaitForSeconds(2f);
        transform.GetComponent<SceneManagement>().loadNextScene();
    }
}
