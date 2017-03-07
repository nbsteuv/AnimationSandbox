using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject[] interactZones;

    private GameControllerScript gameController;
    private GameObject actor = null;

    private Dictionary<GameObject, GameObject> interactZoneSlots;

    // Use this for initialization
    void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        interactZoneSlots = new Dictionary<GameObject, GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        GameObject activeCharacter = gameController.getActiveCharacter();
        if (activeCharacter != null)
        {
            if (findOpenSlot())
            {
                GameObject openSlot = findOpenSlot();
                interactZoneSlots.Add(openSlot, activeCharacter);
                Debug.Log("Moving character");
                activeCharacter.GetComponent<Controllable>().walk(openSlot.transform.position.x, openSlot.transform.position.z);
            }
            actor = activeCharacter;
        }
        else
        {
            Debug.Log("No active character");
        }
        
    }

    GameObject findOpenSlot()
    {
        foreach (GameObject interactZone in interactZones)
        {
            if (!interactZoneSlots.ContainsKey(interactZone))
            {
                return interactZone;
            }
        }
        return null;
    }


}
