using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 100f;

    private Animator anim;

	// Use this for initialization
	void Start ()
	{
	    anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    float forwardMotion = Input.GetAxis("Vertical") * speed * Time.deltaTime;
	    if (forwardMotion != 0)
	    {
	        anim.SetBool("IsWalking", true);
            anim.SetFloat("CharacterSpeed", forwardMotion);
	    }
	    else
	    {
	        anim.SetBool("IsWalking", false);
	    }
	    float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Translate(0, 0, forwardMotion);
        transform.Rotate(0, rotation, 0);

	    if (Input.GetButtonDown("Jump"))
	    {
	        anim.SetTrigger("IsJumping");
	    }
	}
}
