using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/* This is the controller for the weapon AI
 * Should be a slower speed and larger radius than the weapon AI
 * */

public class MrMeeseeksAI : NetworkBehaviour {
    // Inspector Variables
    public GameObject parent;
    [SerializeField]
    float speed;
    [SerializeField]
    float radius;

    // Fixed Variables
    float timeCounter = 0;
    public Vector3 pos;

    void Awake()
    {
        //timeCounter = Time.time;
    }

    void Update() {
        timeCounter += Time.deltaTime * speed;

        // Position of Mr. Meeseeks is reliant on the parent's position
        pos = parent.transform.position;

        // Switch x and z to go clockwise/counter
        float x = pos.x + (Mathf.Sin(timeCounter) * radius);
        float y = 0;
        float z = pos.z + (Mathf.Cos(timeCounter) * radius);

        // Update position of Mr. Meeseeks constantly
        transform.position = new Vector3(x, y, z);

        //if (Time.time - timeCounter > 2)
         //   NetworkServer.Destroy(gameObject);
    }
}
