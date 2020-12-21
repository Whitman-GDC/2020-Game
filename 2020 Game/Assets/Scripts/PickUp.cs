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
    public GameObject player;

	[HideInInspector] public GameObject holding;
    int currentSlot = 0;

    Inventory inventory;

    void Awake()
    {
        interactText.SetActive(false);
        interactText.GetComponent<Text>().text = "Press F to pick up";
    }

	void Start()
	{
        inventory = player.GetComponent<Inventory>();
	}

	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
		{
            currentSlot = 0;
		} else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
            currentSlot = 1;
		} else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentSlot = 2;
        } else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentSlot = 3;
        } else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentSlot = 4;
        }

        for (int i = 0; i < inventory.getInventoryItems().Length; i++)
		{
            if (inventory.getInventoryItems()[i] != null)
			{
                if (i != currentSlot)
			    {
                    inventory.getInventoryItems()[i].GetComponent<MeshRenderer>().enabled = false;
                    inventory.getInventoryItems()[i].GetComponent<Rigidbody>().isKinematic = true;
                    inventory.getInventoryItems()[i].GetComponent<Collider>().isTrigger = true;
			    } else
			    {
                    inventory.getInventoryItems()[i].GetComponent<MeshRenderer>().enabled = true;
                    inventory.getInventoryItems()[i].GetComponent<Rigidbody>().isKinematic = false;
                    inventory.getInventoryItems()[i].GetComponent<Collider>().isTrigger = false;
                }
            }
        }

        holding = inventory.getInventoryItems()[currentSlot];
        Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hit, maxInteractableDistance, interactables))
		{
            // display the hint when raycast hits and interactable object
			interactText.SetActive(true);
            if (Input.GetButton("PickUp"))
			{
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
        if (!inventory.addToInventory(item, currentSlot))
		{
            holding = inventory.getInventoryItems()[currentSlot];
        }

        holding = item;

        // make the item a child of the pickup container and set it to default position
        holding.transform.SetParent(pickUpContainer);
        holding.transform.localPosition = Vector3.zero;
        holding.transform.localRotation = Quaternion.Euler(Vector3.zero);
        holding.transform.localScale = Vector3.one;
    }

    public void Drop()
    {
        // setting parent to environment
        holding.transform.SetParent(environment);
        holding.transform.localScale = inventory.getOriginalScale(currentSlot);

        // add forces to throw away item
        holding.GetComponent<Rigidbody>().AddForce(transform.forward * dropForwardForce, ForceMode.Impulse);
        holding.GetComponent<Rigidbody>().AddForce(transform.up * dropUpwardForce, ForceMode.Impulse);

        holding = null;
        inventory.removeFromInventory(currentSlot);        
    }
}
