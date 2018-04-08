using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player Controller
/// </summary>
public class PlayerControl : MonoBehaviour {
    private int m_health;
    private int m_ammoCount;
    [SerializeField]
    private float m_speed = 2.0f;

    public int Health { get { return m_health; } }
    public int AmmoCount { get { return m_ammoCount; } }
    public float Speed { get { return m_speed; } }
	// Use this for initialization
	void Start () {
        m_health = 100;
        m_ammoCount = 150;
        m_speed = 2.0f;
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
        float posX = transform.position.x;
        float posY = transform.position.y;
        float posZ = transform.position.z;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            var mouseWorldPosition = hit.point;
            transform.LookAt(mouseWorldPosition);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            posX -= m_speed * Time.deltaTime;
            transform.position = new Vector3(posX, posY, posZ);
        }

        if (Input.GetKey(KeyCode.D))
        {
            posX += m_speed * Time.deltaTime;
            transform.position = new Vector3(posX, posY, posZ);
        }

        if (Input.GetKey(KeyCode.S))
        {
            posZ -= m_speed * Time.deltaTime;
            transform.position = new Vector3(posX, posY, posZ);
        }

        if (Input.GetKey(KeyCode.W))
        {
            posZ += m_speed * Time.deltaTime;
            transform.position = new Vector3(posX, posY, posZ);
        }

	}

    //TODO Implement Firing
    //TODO Implement Collision Hit info
    //TODO Implement Everything Else.
    //TODO Implement Dealing with Damage.
}
