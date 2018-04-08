using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrMeeseeksAI : MonoBehaviour {
    float timeCounter = 0;
    float speed;
    float radius;
	public Vector3 pos;

	// Use this for initialization
	void Start () {
        speed = 20;
        radius = 2;
    }

    // Update is called once per frame
    void Update () {
        timeCounter += Time.deltaTime * speed;
		pos = GameObject.FindGameObjectWithTag("Player").transform.position;

		float x = pos.x + (Mathf.Cos(timeCounter) * radius);
        float y = 0;
		float z = pos.z + (Mathf.Sin(timeCounter) * radius);

        transform.position = new Vector3(x, y, z); 
	}
}
