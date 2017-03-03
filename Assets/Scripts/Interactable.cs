using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private GameControllerScript gameController;
    private GameObject actor = null;

    // Use this for initialization
    void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        GameObject activeCharacter = gameController.getActiveCharacter();
        if (activeCharacter != null)
        {
            Debug.Log("Found character, interacting");
            actor = activeCharacter;
        }
        else
        {
            Debug.Log("No active character");
        }
        
    }


}
