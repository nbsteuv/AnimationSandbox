using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private GameControllerScript gameController;

    // Use this for initialization
    void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


}
