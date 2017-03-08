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
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	            RaycastHit hit;
	            if (Physics.Raycast(ray, out hit))
	            {
	                if (hit.collider.gameObject.GetComponent<Interactable>() != null || hit.collider.gameObject.GetComponent<Controllable>() != null)
	                {
	                    return;
	                }
                    
                    Vector3 mousePosition = hit.point;
                    float mousePositionX = mousePosition.x;
                    float mousePositionZ = mousePosition.z;
                    Controllable activeCharacterControllerScript = activeCharacter.GetComponent<Controllable>();
                    if (activeCharacterControllerScript != null)
                    {
                        activeCharacterControllerScript.walk(mousePositionX, mousePositionZ);
                    }
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
