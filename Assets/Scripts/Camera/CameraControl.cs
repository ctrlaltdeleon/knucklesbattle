using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    [SerializeField]
    private float m_speed = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 campos = transform.position;
        Vector3 mousePosition = Input.mousePosition;
        int screenWidth = Camera.main.pixelWidth;
        int screenHeight = Camera.main.pixelHeight;

        if (mousePosition.x < 30)
        {
            campos.x -= m_speed * Time.deltaTime;
        }
        else if (mousePosition.x > screenWidth - 30)
        {
            campos.x += m_speed * Time.deltaTime;
        }

        if (mousePosition.y < 30)
        {
            campos.z -= m_speed * Time.deltaTime;
        }
        else if (mousePosition.y > screenHeight - 30)
        {
            campos.z += m_speed * Time.deltaTime;
        }

        transform.position = campos;
	}
}
