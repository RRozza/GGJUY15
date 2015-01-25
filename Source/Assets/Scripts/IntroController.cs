using UnityEngine;
using System.Collections;

public class IntroController : MonoBehaviour {

	private MovieTexture movie;

	void Start ()
	{
		movie = renderer.material.mainTexture as MovieTexture;
		movie.Play();
	}
	
	void Update ()
	{
		if (!movie.isPlaying) {
			gameObject.transform.position += new Vector3(0,0,15);
			Context.SharedInstance.introFinished = true;
			GameObject.Destroy(gameObject);
		}
	}
}
