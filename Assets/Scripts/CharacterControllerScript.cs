using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 100f;
  

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    float forwardMotion = Input.GetAxis("Vertical") * speed * Time.deltaTime;
	    float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Translate(0, 0, forwardMotion);
        transform.Rotate(0, rotation, 0);
	}
}
