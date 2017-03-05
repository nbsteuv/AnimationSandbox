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
	    if (Input.GetButtonDown("Fire1"))
	    {
	        if (activeCharacter != null)
	        {
                Debug.Log("Active character is not null.");
	            Vector3 mousePosition = Input.mousePosition;
	            float mousePositionX = mousePosition.x;
	            float mousePositionZ = mousePosition.z;
                Debug.Log("Mouse position: " + mousePosition);
                Controllable activeCharacterControllerScript = activeCharacter.GetComponent<Controllable>();
	            if (activeCharacterControllerScript != null)
	            {
                    Debug.Log("Calling active character script walk");
                    activeCharacterControllerScript.walk(mousePositionX, mousePositionZ);
                }
	        }
	    }
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
