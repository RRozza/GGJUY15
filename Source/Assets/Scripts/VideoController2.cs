using UnityEngine;
using System.Collections;

public class VideoController2 : MonoBehaviour {
	
	void Update ()
	{
		if (Context.SharedInstance.gameEnded) {
			if(Context.SharedInstance.winner == Players.NONE) {
				MovieTexture movie = renderer.material.mainTexture as MovieTexture;
				movie.Play ();
			}
		}
	}
}
