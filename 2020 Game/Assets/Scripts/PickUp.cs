using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{

    public LayerMask interactables;
    public float maxInteractableDistance = 15f;
    public float dropUpwardForce = 4f, dropForwardForce = 3f;

    public GameObject interactText;
    public GameObject canvas;

    public Transform pickUpContainer;
	public Transform environment;
    public Transform player;

	GameObject holding;
    Vector3 holdingOriginalScale;

    void Awake()
    {
        interactText.SetActive(false);
        interactText.GetComponent<Text>().text = "Press F to pick up";
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hit, maxInteractableDistance, interactables))
		{
            // display the hint when raycast hits and interactable object
			interactText.SetActive(true);
            if (Input.GetButton("PickUp"))
			{
                if (holding)
                {
					// if player is already holding something, drop it first
					Drop();
				}
                // pick up when the player presses the pickup key
                PickUpObject(hit.collider.gameObject);
            }
		}
		else
		{
			interactText.SetActive(false);
		}

        if (Input.GetButton("Drop"))
		{
            if (holding)
            {
                Drop();
            }
		}

        if (holding)
		{
            holding.transform.localPosition = Vector3.zero;
            holding.transform.localRotation = Quaternion.Euler(Vector3.zero);
            holding.transform.localScale = Vector3.one;
        }
	}

    void PickUpObject(GameObject item)
	{
        // set current holding item to object picked up
        holding = item;
        holdingOriginalScale = item.transform.localScale;

		// make the item a child of the pickup container and set it to default position
		holding.transform.SetParent(pickUpContainer);
        holding.transform.localPosition = Vector3.zero;
        holding.transform.localRotation = Quaternion.Euler(Vector3.zero);
        holding.transform.localScale = Vector3.one;
    }

    void Drop()
    {
        // setting parent to null
        holding.transform.SetParent(environment);

        // add forces to throw away item
        holding.GetComponent<Rigidbody>().AddForce(transform.forward * dropForwardForce, ForceMode.Impulse);
        holding.GetComponent<Rigidbody>().AddForce(transform.up * dropUpwardForce, ForceMode.Impulse);

        holding.transform.localScale = holdingOriginalScale;
        holding = null;
    }
}
