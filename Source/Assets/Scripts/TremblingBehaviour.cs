using UnityEngine;
using System.Collections;

public class TremblingBehaviour : MonoBehaviour
{
	
		public float speed;
		public int limit;
		private int position = 0;
		private bool direction = true;
    
	
		// Update is called once per frame
		void Update ()
		{
				if (Context.SharedInstance.trembleEnabled == true) {
				
						if (position < (-1 * limit)) {
								direction = true;	
						} 
						if (position > limit) {
								direction = false;
						}
		
						if (direction) {
								transform.position += (Vector3.left * speed * Time.deltaTime);
								position++;
						} else {
								transform.position += (Vector3.right * speed * Time.deltaTime);
								position--;
						}
				}
		}
}
