using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrMeeseeksAI : MonoBehaviour {
    float timeCounter = 0;
    float speed;
    float radius;
	public Vector3 pos;
    public GameObject parent;

	// Use this for initialization
	void Start () {
        speed = 5;
        radius = 4;
    }

    // Update is called once per frame
    void Update () {
        timeCounter += Time.deltaTime * speed;

		pos = parent.transform.position;

        // Switch x and z to go clockwise/counter
		float x = pos.x + (Mathf.Sin(timeCounter) * radius);
        float y = 0;
		float z = pos.z + (Mathf.Cos(timeCounter) * radius);

        transform.position = new Vector3(x, y, z); 
	}
}
