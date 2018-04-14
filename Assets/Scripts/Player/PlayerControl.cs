using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player Controller
/// </summary>
public class PlayerControl : MonoBehaviour {
    public enum WeaponType : uint
    {
        DEFAULT = 0,
        MESEEKS = 1,
        SPECIAL = 2
    }

    [SerializeField]
    private WeaponType m_weaponType;

    [SerializeField]
    private int m_health;

    [SerializeField]
    private int m_ammoCount = 150;

    [SerializeField]
    private float m_speed = 2.0f;

    public int Health { get { return m_health; } }
    public int AmmoCount { get { return m_ammoCount; } }
    public float Speed { get { return m_speed; } }
    public WeaponType Weapon { get { return m_weaponType; } }

    [SerializeField]
    private GameObject m_bulletPrefab;

	// Use this for initialization
	void Start () {
        m_health = 100;
        m_ammoCount = 150;
        m_speed = 2.0f;
        m_weaponType = WeaponType.DEFAULT;
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

        if (Input.GetMouseButtonDown(0))
        {
            if (m_weaponType == WeaponType.DEFAULT || m_weaponType == WeaponType.SPECIAL)
            {
                if (m_ammoCount > 0)
                {
                    StartCoroutine(Shoot());
                }
            }
            else {
                StartCoroutine(DeployMeeseeks());
            }
        }
	}

    /// <summary>
    /// Shoots this instance.
    /// </summary>
    /// <returns></returns>
    IEnumerator Shoot()
    {
        PlayerBullet newBullet = Instantiate(m_bulletPrefab).GetComponent<PlayerBullet>();
        newBullet.SetInitialDirection(transform.forward);
        var spawnPoint = transform.position;
        spawnPoint.y = spawnPoint.y + 0.25f;
        newBullet.transform.position = spawnPoint;
        --m_ammoCount;
        yield return new WaitForSeconds(0.01f);
    }

    IEnumerator DeployMeeseeks()
    {
        //TODO Implement this with Meseeks.
        yield return new WaitForSeconds(0.01f);
    }

    /// <summary>
    /// Rewards the health.
    /// </summary>
    /// <param name="value">The value.</param>
    public void RewardHealth(int value)
    {
        m_health += value;
    }

    /// <summary>
    /// Rewards the meseeks.
    /// </summary>
    public void RewardMeseeks()
    {

    }

    /// <summary>
    /// Rewards the ammo.
    /// </summary>
    /// <param name="value">The value.</param>
    public void RewardAmmo(int value)
    {
        m_ammoCount += value;
    }

    /// <summary>
    /// Rewards the special weapon.
    /// </summary>
    public void RewardSpecialWeapon()
    {

    }
    //TODO Implement Collision Hit info

}
