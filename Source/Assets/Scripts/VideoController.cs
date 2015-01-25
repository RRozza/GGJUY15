using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class VideoController : MonoBehaviour
{
	private MovieTexture movie;

	void Start () {
		movie = renderer.material.mainTexture as MovieTexture;
	}

	void Update ()
	{
		if (Context.SharedInstance.gameStarted && !Context.SharedInstance.gameEnded) {
				if (!movie.isPlaying) {
					movie.Play ();
					audio.Play ();
				}				
		}

		if (Context.SharedInstance.gameEnded) {
			movie.Stop ();
			audio.Stop ();
			gameObject.transform.position += new Vector3 (0, 0, 15);
			GameObject.Destroy(gameObject);
		}

	}

}
