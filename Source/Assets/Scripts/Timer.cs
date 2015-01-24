using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
    int min = 0;
	int sec=0;
	int fraction=0;
	float timecount=0;
	float starttime=0;
	string timeCounter="";
	// Use this for initialization
	void Start () {
		starttime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	
		timecount = Time.time - starttime;
		min = (int)(timecount/60f);
		sec = (int)(timecount % 60f);
		fraction = (int)((timecount * 10) %10);
		timeCounter = "minutes: "+min.ToString()+" seconds: "+sec.ToString()+" fraction: "+fraction.ToString();
		Debug.Log(timeCounter);

	}
}
