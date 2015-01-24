using UnityEngine;
using System.Collections;

public class RegisterGO : MonoBehaviour {

	void Start()
	{
		Registry.Add(gameObject);
	}

	void OnDestroy()
	{
		Registry.Remove(gameObject);
	}
}
