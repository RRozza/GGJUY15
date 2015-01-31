using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BackgroundController : MonoBehaviour {
	private MovieTexture movie;
	
	void Start () {
		movie = renderer.material.mainTexture as MovieTexture;
	}
	
	void Update () {
		if (Context.Instance.GameState() == GameStates.STARTED) {
			if (!movie.isPlaying) {
				movie.Play ();
				audio.Play ();
			}				
		} else {
			if (Context.Instance.GameState() == GameStates.ENDED) {
				audio.Stop ();
				movie.Stop ();
				gameObject.SetActive (false);
				GameObject.Destroy (gameObject);
			}
		}
	}
}
