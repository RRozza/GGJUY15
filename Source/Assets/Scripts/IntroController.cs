using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class IntroController : MonoBehaviour {

	private MovieTexture movie;

	void Start () {
		movie = renderer.material.mainTexture as MovieTexture;
		movie.Play();
		audio.Play();
	}
	
	void Update () {
		if (Context.Instance.GameState() == GameStates.INTRO && !movie.isPlaying) {
			movie.Stop();
			audio.Stop();
			//gameObject.transform.position += new Vector3(0,0,15);
			gameObject.SetActive(false);
			GameObject.Destroy(gameObject);
			Context.Instance.SetGameState(GameStates.START);
		}
	}
}
