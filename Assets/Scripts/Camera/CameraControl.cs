using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    [SerializeField]
    private float m_speed = 0.5f;

    [SerializeField]
    private float m_lerpFactor = 0.8f;

    [SerializeField]
    private GameObject m_player;

    [SerializeField]
    private Vector3 m_focalPosition;

    private GameObject m_rotatePoint;

    private bool m_hasRotated;

    [SerializeField]
    private int m_rotationIndex;

    private Vector3[] m_rotatePoints = {
        new Vector3(0.0f, 0f, 0.0f),
        new Vector3(0.0f, 90f, 0.0f),
        new Vector3(0.0f, 180f, 0.0f),
        new Vector3(0.0f, 270f, 0f)
    };

	// Use this for initialization
	void Start () {
        if (m_player != null)
        {
            m_focalPosition = m_player.transform.position;
        }
        m_rotatePoint = new GameObject("CameraRotatePoint");
        transform.parent = m_rotatePoint.transform;
        m_hasRotated = false;
        m_rotationIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 campos = transform.position;
        Vector3 oldRotation = transform.rotation.eulerAngles;
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!m_hasRotated)
            {
                m_rotationIndex--;
                GameManager.Instance.ChangeCoordinateDirection(m_rotationIndex);
                m_hasRotated = true;
            }

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!m_hasRotated)
            {
                m_rotationIndex++;
                GameManager.Instance.ChangeCoordinateDirection(m_rotationIndex);
                m_hasRotated = true;
            }

        }

        var newRotation = m_rotatePoints[(int)Mathf.Abs(m_rotationIndex % 4)];
        if (oldRotation.y != newRotation.y)
        {
            if (oldRotation.y > newRotation.y + 1.0f || oldRotation.y < newRotation.y - 1.0f)
            {
                var newRot = Vector3.Slerp(oldRotation, newRotation, 0.8f);
                newRot.x = 0;
                newRot.z = 0;
                m_rotatePoint.transform.rotation = Quaternion.Euler(newRot);
            }
            else
            {
                m_rotatePoint.transform.rotation = Quaternion.Euler(newRotation);
                m_hasRotated = false;
            }
        }
        else {
            m_hasRotated = false;
        }
	}
}
