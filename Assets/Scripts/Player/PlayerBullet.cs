using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBullet : NetworkBehaviour
{
    [SerializeField] private float m_speed = 1f;
    [SerializeField] private float max_speed = 100f;

    [SerializeField] private int bulletDamage = 10;

    public float timer = 0;

    [SerializeField]
    public int BulletDamage
    {
        get { return bulletDamage; }
    }

    private Rigidbody m_rigidBody;


    void Awake()
    {
        timer = Time.time;
    }

    void Start()
    {
        Debug.Log("Rotation" + transform.rotation);
        m_rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        m_rigidBody.AddForce(transform.forward * m_speed);
        if (Time.time - timer > 2)
            NetworkServer.Destroy(gameObject);
        if (m_speed < max_speed)
        {
            m_speed += m_speed;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}