using UnityEngine;
using System.Collections;

public class StrartController : MonoBehaviour {

	private MovieTexture movie;

	void Start ()
	{
		movie = renderer.material.mainTexture as MovieTexture;
		movie.loop = true;
	}

	void Update () {
		if (Context.SharedInstance.introFinished && !movie.isPlaying) {
			movie.Play ();
		}

		if (Input.anyKey) {
			movie.Stop();
			gameObject.transform.position += new Vector3(0,0,15);
			Context.SharedInstance.gameStarted = true;
			Context.SharedInstance.startTime = Time.time;
			GameObject.Destroy(gameObject);
		}
	}
}
