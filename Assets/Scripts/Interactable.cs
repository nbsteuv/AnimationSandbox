using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject[] interactZones;
    public enum Interaction
    {
        Sit,
        Jump
    }
    public Interaction interaction;
    public float interactDistance = 0.6f;

    private GameControllerScript gameController;

    private Dictionary<GameObject, GameObject> interactZoneSlots;
    private Dictionary<GameObject, GameObject> slotsByActor;
    private List<GameObject> watchForCloseDistance;

    // Use this for initialization
    void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        interactZoneSlots = new Dictionary<GameObject, GameObject>();
        slotsByActor = new Dictionary<GameObject, GameObject>();
        watchForCloseDistance = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    checkActorDistances();
	}

    void OnMouseDown()
    {
        Debug.Log("Clicked");
        GameObject activeCharacter = gameController.getActiveCharacter();
        if (activeCharacter != null && !slotsByActor.ContainsKey(activeCharacter))
        {
            if (findOpenSlot())
            {
                GameObject openSlot = findOpenSlot();
                interactZoneSlots.Add(openSlot, activeCharacter);
                slotsByActor.Add(activeCharacter, openSlot);
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

    void interact(GameObject actor, GameObject slot)
    {
        switch (interaction)
        {
            case Interaction.Sit:
                sit(actor, slot);
                break;
            case Interaction.Jump:
                Debug.Log("jump");
                break;
        }
        
    }

    void checkActorDistances()
    {
        List<GameObject> removeList = new List<GameObject>();
        foreach (GameObject slot in watchForCloseDistance)
        {
            GameObject actor = interactZoneSlots[slot];
            Debug.Log(Vector3.Distance(actor.transform.position, slot.transform.position));
            if (Vector3.Distance(actor.transform.position, slot.transform.position) < interactDistance)
            {
                removeList.Add(slot);
                actor.GetComponent<Controllable>().setController(gameObject);
                interact(actor, slot);
            }
        }
        foreach (GameObject slot in removeList)
        {
            watchForCloseDistance.Remove(slot);
        }
    }

    void sit(GameObject actor, GameObject slot)
    {
        Debug.Log("Rotate");
        Debug.Log(slot.transform.rotation);
        Controllable actorControl = actor.GetComponent<Controllable>();
        actorControl.setInteracting(1);
        //TODO: Lerp and Slerp in coroutines here
        actor.transform.rotation = slot.transform.rotation;
        actor.transform.position = slot.transform.position;
        Debug.Log("Sit");
        actor.GetComponent<Animator>().SetBool("IsSitting", true);
    }
}
