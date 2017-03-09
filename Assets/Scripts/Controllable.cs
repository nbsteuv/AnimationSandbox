using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Resources;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    public GameObject haloPrefab;
    public Vector3 haloOffset;
    public float selectToActionWait = 0.05f;
    public float speed = 2;
    public float rotationSpeed = 5f;

    private GameControllerScript gameController;
    private Animator anim;
    private float timeUntilAction = 0;
    private bool active = false;
    private GameObject halo = null;

    private GameObject controller = null;

    private Vector3 targetPosition;

    private Quaternion targetDirection;
    private float rotationTime;
    private float rotationCurve;
    private bool interacting;

    private bool animationLock = false;
    private bool endingInteraction = false;

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

	    if (active && turnToTarget())
	    {
	        moveToTarget();
	    }
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
            if (controller == null)
            {
                targetPosition = new Vector3(locationX, transform.position.y, locationZ);
                targetDirection = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);
            }
        }
    }

    private void moveToTarget()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 0.01)
        {
            anim.SetBool("IsWalking", true);
            float percentageCovered = (Time.deltaTime * speed) / Vector3.Distance(transform.position, targetPosition);
            Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, percentageCovered);
            transform.position = newPosition;
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }

    private bool turnToTarget()
    {
        if (interacting)
        {
            return true;
        }

        if (targetDirection != transform.rotation)
        {
            anim.SetBool("IsWalking", true);

            Quaternion newRotation;

            rotationTime += Time.deltaTime;
            float newRotationCurve = Mathf.Abs(Mathf.Sin(rotationTime * rotationSpeed));
            
            if (newRotationCurve > rotationCurve && newRotationCurve < 0.97f)
            {
                rotationCurve = newRotationCurve;
                newRotation = Quaternion.Slerp(transform.rotation, targetDirection,
                rotationCurve);
            }
            else
            {
                newRotation = targetDirection;
            }

            transform.rotation = newRotation;
            return false;
        }
        else
        {
            rotationTime = 0;
            rotationCurve = 0;
            return true;
        }
    }

    public void setController(GameObject newController)
    {
        controller = newController;
    }

    public void setInteracting(int isInteracting)
    {
        if (isInteracting > 0)
        {
            interacting = true;
        }
        else
        {
            interacting = false;
        }
    }

    public void setAnimationLock(int IsAnimationLock)
    {
        if(IsAnimationLock > 0)
        {
            Debug.Log("Animation lock on");
            animationLock = true;
        }
        else
        {
            Debug.Log("Animation lock off");
            animationLock = false;
            if (endingInteraction)
            {
                controller.GetComponent<Interactable>().deregister(gameObject);
                setController(null);
                endingInteraction = false;
                setInteracting(0);
            }
        }
    }

    public bool getAnimationLock()
    {
        return animationLock;
    }

    public GameObject getController()
    {
        return controller;
    }

    public void endInteraction()
    {
        endingInteraction = true;
    }

}
