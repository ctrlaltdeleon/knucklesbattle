using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrMeeseeksAI : MonoBehaviour {
    float timeCounter = 0;
    float speed;
    float radius;

	// Use this for initialization
	void Start () {
        speed = 2;
        radius = 5;
	}
	
	// Update is called once per frame
	void Update () {
        timeCounter += Time.deltaTime * speed;

        float x = (Mathf.Cos(timeCounter) * radius);
        float y = 0;
        float z = (Mathf.Sin(timeCounter) * radius);

        transform.position = new Vector3(x, y, z); 
	}
}
