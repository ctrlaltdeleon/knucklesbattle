using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/* This should be a higher speed than the AI as well as a smaller radius
 * This does the damage as Mr Meeseeks in invulnerable
 * */

public class MrMeeseeksWeaponAI : NetworkBehaviour {
    // Inspector Variables
    public GameObject parent;
    [SerializeField]
    float speed;
    [SerializeField]
    float radius;
    [SerializeField]
    int meeseeksDamage = 20;

    // Fixed Variables
    float timeCounter = 0;
    public Vector3 pos;

    [SerializeField]
    public int MeeseeksDamage
    {
        get { return meeseeksDamage; }
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
    }
}
