using UnityEngine;
using System.Collections;

public class VideoController : MonoBehaviour
{
	void Update ()
	{
		if (Context.SharedInstance.gameStarted && !Context.SharedInstance.gameEnded) {
				MovieTexture movie = renderer.material.mainTexture as MovieTexture;
				if (!movie.isPlaying) {
						movie.Play ();
				}				
		}

		if (Context.SharedInstance.gameEnded) {
			gameObject.transform.position += new Vector3 (0, 0, 15);
			GameObject.Destroy(gameObject);
		}

	}

}
