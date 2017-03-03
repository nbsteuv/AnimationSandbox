using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    private GameControllerScript gameController;
    private bool active = false;
    
	// Use this for initialization
	void Start ()
	{
	    gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        gameController.setActiveCharacter(gameObject);
    }

    public void activate()
    {
        Debug.Log("Activated");
        active = true;
    }

    public void deactivate()
    {
        Debug.Log("Deactivated");
    }

}
