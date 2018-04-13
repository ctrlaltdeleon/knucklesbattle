using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This file will be replaced by the actual player

public class ProtoPlayerMovement : MonoBehaviour {

    float speed;

	// Use this for initialization
	void Start () {
        speed = 7f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(speed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, speed * Input.GetAxis("Vertical") * Time.deltaTime);	
	}
}
