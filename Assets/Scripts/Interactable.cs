using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject[] interactZones;
    public float interactDistance = 0.6f;

    private GameControllerScript gameController;

    private Dictionary<GameObject, GameObject> interactZoneSlots;

    private List<GameObject> watchForCloseDistance;

    // Use this for initialization
    void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        interactZoneSlots = new Dictionary<GameObject, GameObject>();
        watchForCloseDistance = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    checkActorDistances();
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
                watchForCloseDistance.Add(openSlot);
                activeCharacter.GetComponent<Controllable>().walk(openSlot.transform.position.x, openSlot.transform.position.z);
            }
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

    void interact(GameObject actor)
    {
        Debug.Log("Interacting");
    }

    void checkActorDistances()
    {
        List<GameObject> removeList = new List<GameObject>();
        foreach (GameObject slot in watchForCloseDistance)
        {
            GameObject actor = interactZoneSlots[slot];
            if (Vector3.Distance(actor.transform.position, slot.transform.position) < interactDistance)
            {
                removeList.Add(slot);
                actor.GetComponent<Controllable>().setController(gameObject);
                interact(actor);
            }
        }
        foreach (GameObject slot in removeList)
        {
            watchForCloseDistance.Remove(slot);
        }
    }


}
