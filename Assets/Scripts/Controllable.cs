using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    private bool selected = false;
    private Animator anim;
    
	// Use this for initialization
	void Start ()
	{
	    anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        getControl();
    }

    void getControl()
    {
        selected = true;
    }

}
