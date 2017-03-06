using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    public GameObject haloPrefab;
    public Vector3 haloOffset;
    public float selectToActionWait = 0.05f;
    public float speed = 2;
    public float rotationDampen = 55f;

    private GameControllerScript gameController;
    private Animator anim;
    private float timeUntilAction = 0;
    private bool active = false;
    private GameObject halo = null;

    private Vector3 targetPosition;
    private float moveStartTime;
    private Vector3 moveStartPosition;

    private Quaternion targetDirection;
    private float rotationStartTime;
    private Quaternion rotationStartDirection;

    
	// Use this for initialization
	void Start ()
	{
	    gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();
	    anim = GetComponent<Animator>();
	    targetPosition = transform.position;
	    targetDirection = transform.rotation;

	}
	
	// Update is called once per frame
	void Update () {
        if (timeUntilAction > 0)
        {
            timeUntilAction -= Time.deltaTime;
        }

	    moveToTarget();
	}

    void OnMouseDown()
    {
        gameController.setActiveCharacter(gameObject);
    }

    public void activate()
    {
        halo = Instantiate(haloPrefab, transform.position + haloOffset, Quaternion.identity);
        halo.transform.parent = transform;
        timeUntilAction = selectToActionWait;
        active = true;
    }

    public void deactivate()
    {
        DestroyObject(halo);
        active = false;
    }

    public void walk(float locationX, float locationZ)
    {
        if (timeUntilAction <= 0)
        {
            moveStartTime = Time.time;
            moveStartPosition = transform.position;
            targetPosition = new Vector3(locationX, transform.position.y, locationZ);
            targetDirection = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);
        }
    }

    private void moveToTarget()
    {
        if (active && targetDirection != transform.rotation)
        {
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetDirection,
                1f - (Time.deltaTime * rotationDampen));
            transform.rotation = newRotation;
        }

        if (active && Vector3.Distance(transform.position, targetPosition) > 0.01)
        {
            anim.SetBool("IsWalking", true);
            float percentageCovered = ((Time.time - moveStartTime) * speed) / Vector3.Distance(moveStartPosition, targetPosition);
            Vector3 newPosition = Vector3.Lerp(moveStartPosition, targetPosition, percentageCovered);
            transform.position = newPosition;
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }

}
