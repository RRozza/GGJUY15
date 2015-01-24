using UnityEngine;
using System.Collections;

public class CollisionParachute : MonoBehaviour 
{
	void OnTriggerEnter2D (Collider2D collision)
	{
		//Desactivacion al agarrar el parachute
		if (collision.gameObject.CompareTag ("Player"))
			gameObject.SetActive (false);	
	}
}
