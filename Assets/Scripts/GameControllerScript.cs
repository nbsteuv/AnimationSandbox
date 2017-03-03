using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{

    private GameObject activeCharacter = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setActiveCharacter(GameObject newActiveCharacter)
    {
        if (activeCharacter != null)
        {
            activeCharacter.GetComponent<Controllable>().deactivate();
        }
        
        activeCharacter = newActiveCharacter;
        activeCharacter.GetComponent<Controllable>().activate();
    }

    public GameObject getActiveCharacter()
    {
        return activeCharacter;
    }

}
