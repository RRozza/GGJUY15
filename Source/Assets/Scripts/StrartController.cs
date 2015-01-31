using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class StrartController : MonoBehaviour {

	private MovieTexture movie;

	void Start () {
		movie = renderer.material.mainTexture as MovieTexture;
		movie.loop = true;
	}

	void Update () {
		if ((Context.Instance.GameState() == GameStates.START) && (!movie.isPlaying)) {
			movie.Play ();
			audio.Play();
		}

		if ((Context.Instance.GameState() == GameStates.START) && (Input.anyKeyDown)) {
			movie.Stop();
			audio.Stop();
			gameObject.SetActive(false);
			GameObject.Destroy(gameObject);
			Context.Instance.SetStartTime(Time.time); 
			Context.Instance.SetGameState(GameStates.STARTED);
		}
	}
}
