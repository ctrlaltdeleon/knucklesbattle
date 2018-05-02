using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBullet : NetworkBehaviour
{
    private Vector3 m_direction;

    [SerializeField] private float m_speed = 100f;

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

    /// <summary>
    /// Sets Initial direction of Bullet based off Parent's forward vector.
    /// </summary>
    /// <param name="direction"></param>
    public void SetInitialDirection(Vector3 direction)
    {
        m_direction = direction;
    }

    // Use this for initialization

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        m_rigidBody.AddForce(m_direction * m_speed);
        if (Time.time - timer > 2)
            NetworkServer.Destroy(gameObject);
    }
}