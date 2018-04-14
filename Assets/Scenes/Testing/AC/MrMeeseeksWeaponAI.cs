using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrMeeseeksWeaponAI : MonoBehaviour {
    // Inspector Variables
    public GameObject parent;
    [SerializeField]
    float speed;
    [SerializeField]
    float radius;

    // Fixed Variables
    float timeCounter = 0;
    public Vector3 pos;

    void Update() {
        timeCounter += Time.deltaTime * speed;

        pos = parent.transform.position;

        // Switch x and z to go clockwise/counter
        float x = pos.x + (Mathf.Sin(timeCounter) * radius);
        float y = 0;
        float z = pos.z + (Mathf.Cos(timeCounter) * radius);

        transform.position = new Vector3(x, y, z);
    }
}
