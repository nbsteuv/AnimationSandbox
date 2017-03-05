using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    public GameObject haloPrefab;
    public Vector3 haloOffset;
    public float speed;

    private GameControllerScript gameController;
    private Animator anim;
    private bool active = false;
    private GameObject halo = null;

    private Vector3 targetPosition;
    private float moveStartTime;
    private Vector3 moveStartPosition;

    
	// Use this for initialization
	void Start ()
	{
	    gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();
	    anim = GetComponent<Animator>();
	    targetPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	    if (targetPosition != transform.position)
	    {
	        anim.SetBool("IsWalking", true);
	        float percentageCovered = ((Time.time - moveStartTime) * speed) / Vector3.Distance(moveStartPosition, targetPosition);
            Debug.Log(percentageCovered);
	        transform.position = Vector3.Lerp(moveStartPosition, targetPosition, percentageCovered);
	    }
	    else
	    {
            anim.SetBool("IsWalking", false);
        }
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

    public void walk(float locationX, float locationZ)
    {
        Debug.Log("Walk called");
        moveStartTime = Time.time;
        moveStartPosition = transform.position;
        Debug.Log("Move start position: " + moveStartPosition);
        targetPosition = new Vector3(locationX, transform.position.y, locationZ);
        Debug.Log("Target position: " + targetPosition);
    }

}
