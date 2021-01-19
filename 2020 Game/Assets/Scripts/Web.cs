using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{

	public Rigidbody player;

	public float speedMultiplier = 0.1f;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponent<PlayerMovement>().CaughtInWeb(speedMultiplier);
		}
	}

	//private void OnTriggerStay(Collider other)
	//{
	//	if (other.gameObject.CompareTag("Player"))
	//	{
	//		other.gameObject.GetComponent<PlayerMovement>().CaughtInWeb(speedMultiplier);
	//	}
	//}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponent<PlayerMovement>().LeaveWeb(speedMultiplier);
		}
	}
}
