using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    private Vector3 m_direction;

    [SerializeField]
    private float m_speed = 0.5f;

    /// <summary>
    /// Sets Initial direction of Bullet based off Parent's forward vector.
    /// </summary>
    /// <param name="direction"></param>
    public void SetInitialDirection(Vector3 direction)
    {
        m_direction = direction;
    }

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_direction != null)
        {
            Vector3 movement = new Vector3(m_speed * m_direction.x, m_speed * m_direction.y, m_speed * m_direction.z);
            transform.Translate(movement);
        }
	}

    private void OnBecameInvisible()
    {
        Destroy(transform.gameObject);
    }
}
