using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpenScript : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = this.transform.parent.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider obj)
    {
        anim.SetBool("IsOpening", true);
    }

    void OnTriggerExit(Collider obj)
    {
        anim.SetBool("IsOpening", false);
    }
}
