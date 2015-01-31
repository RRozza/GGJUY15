using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class EndController : MonoBehaviour {
	private int timer;
	private bool stoped;
	private MovieTexture movie;

	public int end;
	public Players player;

	void Play() {
		audio.Play ();
		movie.Play(); 			
	}

	void Stop() {
		audio.Stop ();
		movie.Stop ();
	}

	void Start () {
		timer = 0;
		stoped = false;
		movie = renderer.material.mainTexture as MovieTexture;
	}	
	
	void Update () {
		if (!stoped) {
			if (Context.Instance.GameEnded()) {
				if (Context.Instance.Winner() == player) {
					if (!movie.isPlaying) {
						gameObject.transform.position -= new Vector3 (0, 0, 2);
						Play();
					}

					timer++;
					if (timer == end) {
						Stop();
						stoped = true;
					}
				}
			}
		} else {
			if (Input.anyKeyDown) {
				Application.LoadLevel(0);
			}
		}
	}
}
