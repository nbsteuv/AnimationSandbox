using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    public GameObject haloPrefab;
    public Vector3 haloOffset;

    private GameControllerScript gameController;
    private bool active = false;
    private GameObject halo = null;
    
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
        halo = Instantiate(haloPrefab, transform.position + haloOffset, Quaternion.identity);
        active = true;
    }

    public void deactivate()
    {
        DestroyObject(halo);
        active = false;
    }

}
