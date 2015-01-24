using UnityEngine;
using System.Collections;

public class ObstacleAsension : MonoBehaviour
{

		public float speed;
		public int limit;
		private int position = 0;
		private bool direction = true;
		public float xStart, yStart;
		public float xMin, xMax, yMin, yMax;
		public int screenTop;

	
		// Use this for initialization
		void Start ()
		{
		
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (this.transform.position.y <= screenTop + 3) {
						transform.position += (Vector3.up * speed * Time.deltaTime);
				}
		}
}
