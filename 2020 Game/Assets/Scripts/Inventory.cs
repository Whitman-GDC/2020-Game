using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Text itemsDisplay;

    private GameObject[] items = new GameObject[5];
    private string[] itemsNames = new string[5];
    private Vector3[] originalScales = new Vector3[5];

    public PickUp pickUp;

    // Update is called once per frame
    void Update()
    {
        updateUI();
    }

    public bool addToInventory(GameObject item, int slot)
	{
        bool added = false;
        if (items[slot] == null)
		{
            items[slot] = item;
            itemsNames[slot] = item.tag;
            originalScales[slot] = item.transform.localScale;
            added = true;
		}

        if (!added)
		{
            for (int i = 0; i < items.Length; i++)
			{
                if (items[i] == null)
				{
                    items[i] = item;
                    itemsNames[i] = item.tag;
                    originalScales[i] = item.transform.localScale;
                    added = true;
                    break;
                }
			}
		}

        if (!added)
        {
            pickUp.Drop();
            items[slot] = item;
            itemsNames[slot] = item.tag;
            originalScales[slot] = item.transform.localScale;
            return false;
        }

        return true;
	}

    public void removeFromInventory(int slot)
	{
        items[slot] = null;
        itemsNames[slot] = null;
        originalScales[slot] = Vector3.zero;
    }

    public GameObject[] getInventoryItems()
	{
        return items;
	}

    public string[] getInventoryItemsNames()
	{
        return itemsNames;
	}

    void updateUI()
	{
        string displayText = "";
        for (int i = 0; i < itemsNames.Length; i++)
		{
            if (i != itemsNames.Length - 1)
            {
                displayText += (i + 1) + ". " + itemsNames[i] + ", ";
            } else
			{
                displayText += (i + 1) + ". " + itemsNames[i];
            }
		}

        itemsDisplay.text = displayText;
	}

    public Vector3 getOriginalScale(int slot)
	{
        return originalScales[slot];
	}
}
