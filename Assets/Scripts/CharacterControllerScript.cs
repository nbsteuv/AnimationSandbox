using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 100f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float speedMax = 20f;

    private float currentSpeed = 0.0f;
    private bool isRunning = false;
    private Animator anim;

	// Use this for initialization
	void Start ()
	{
	    anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (isRunning && anim.GetBool("IsWalking"))
	    {
	        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
	        {
	            currentSpeed += (Input.GetAxis("Vertical") * currentSpeed + acceleration) * Time.deltaTime;
	            if (currentSpeed > speedMax)
	            {
	                currentSpeed = speedMax;
	            }
	        }
	        else
	        {
                currentSpeed -= deceleration * Time.deltaTime;
                if (currentSpeed < 0)
                {
                    currentSpeed = 0;
                }
            }
	    }
	    else
	    {
	        currentSpeed = Input.GetAxis("Vertical") * speed;
        }
	    if (currentSpeed != 0)
	    {
	        anim.SetBool("IsWalking", true);
            anim.SetFloat("CharacterSpeed", currentSpeed);
	    }
	    else
	    {
	        anim.SetBool("IsWalking", false);
	    }

	    float forwardMotion = currentSpeed * Time.deltaTime;
	    float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        transform.Translate(0, 0, forwardMotion);
        transform.Rotate(0, rotation, 0);

	    if (Input.GetButtonDown("Jump"))
	    {
	        anim.SetTrigger("IsJumping");
	    }

	    if (Input.GetKeyDown("r"))
	    {
	        isRunning = !isRunning;
	        anim.SetBool("IsRunning", true);
	    }
	}
}
