using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnucklesTestController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().materials[0].color = Color.yellow;
	}
	
	// Update is called once per frame
	void Update () {
		

		if (Input.GetKey(KeyCode.LeftShift))
		{
			var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
			var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
			
			transform.Rotate(0, x, 0);
			transform.Translate(0, 0, z*10);
		}

		
	}
}
