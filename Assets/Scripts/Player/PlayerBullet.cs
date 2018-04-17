using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    private Vector3 m_direction;

    [SerializeField]
    private float m_speed = 0.5f;

	[SerializeField]
	private int bulletDamage = 10;

	[SerializeField]
	public int BulletDamage { get { return bulletDamage; } }

    private Rigidbody m_rigidBody;

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
        m_rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_direction != null)
        {
            m_rigidBody.AddForce(m_direction.x * m_speed, m_direction.y * m_speed, m_direction.z * m_speed);
        }
	}

    private void OnBecameInvisible()
    {
        Destroy(transform.gameObject);
    }
}
