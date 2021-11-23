using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneCameraMovement : MonoBehaviour
{
    public AnimationClip animClip;

    private Animation anim;
    private void Awake()
    {
        anim = GetComponent<Animation>();
        anim.Play(animClip.name);
    }
}
