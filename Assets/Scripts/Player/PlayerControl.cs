using System.Collections;
using System.Collections.Generic;
using Prototype.NetworkLobby;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Player Controller
/// </summary>
public class PlayerControl : NetworkBehaviour
{
    public enum WeaponType : uint
    {
        DEFAULT = 0,
        MESEEKS = 1,
        SPECIAL = 2
    }

    [SerializeField] private WeaponType m_weaponType;

    [SerializeField] private int m_health;
    private int MAX_HEALTH = 100;

    [SerializeField] private int m_ammoCount = 150;
    private int MAX_AMMO = 150;


    public int Health
    {
        get { return m_health; }
    }

    public int MaxHealth
    {
        get { return MAX_HEALTH; }
    }

    public int AmmoCount
    {
        get { return m_ammoCount; }
        set { m_ammoCount = value; }
    }

    public int MaxAmmo
    {
        get { return MAX_AMMO; }
    }

    public WeaponType Weapon
    {
        get { return m_weaponType; }
    }

    public static PlayerControl Instance;
    [SerializeField] private GameObject m_bulletPrefab;
    private Vector3 m_lastPosition;

    private Rigidbody m_rigidBody;

    private Vector3 m_Left;
    private Vector3 m_Forward;
    public Transform bulletSpawnPosition;

    public AudioClip[] rickisms;
    public AudioSource audioSource;
    public float knockbackStrength;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        transform.position = new Vector3(0, 0, -3f);
    }

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            Instance = this;
            CameraController.character = gameObject;
        }

        audioSource = GetComponent<AudioSource>();
        m_health = 100;
        m_ammoCount = 150;
        m_weaponType = WeaponType.DEFAULT;
        m_rigidBody = GetComponent<Rigidbody>();
        m_Left = Vector3.left;
        m_Forward = Vector3.forward;

        InvokeRepeating("PlayRickSounds", Random.Range(3f, 10f), Random.Range(8f, 19f));
    }

    public void PlayRickSounds()
    {
        audioSource.PlayOneShot(rickisms[Random.Range(0, rickisms.Length)]);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Movements
        float xDir = x * Time.deltaTime * 15.0f;
        float zDir = z * Time.deltaTime * 15.0f;
        transform.Translate(xDir, 0, zDir);

        if (Input.GetMouseButtonDown(0))
        {
            if (m_weaponType == WeaponType.DEFAULT || m_weaponType == WeaponType.SPECIAL)
            {
                if (m_ammoCount > 0)
                {
                    CmdShoot(Camera.main.transform.forward, Camera.main.transform.rotation);
                    --m_ammoCount;
                }
            }
            else
            {
                StartCoroutine(DeployMeeseeks());
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (isLocalPlayer)
            {
                m_health -= 10;
                Debug.Log("Health: " + m_health);

                //ApplyKnockback(other.gameObject.transform.position);
                if (m_health < 1)
                {
                    Cursor.visible = true;
                    CmdLoseGame();
                }
            }
        }
    }

    [Command]
    public void CmdLoseGame()
    {
        RpcLoseGame();
    }

    [ClientRpc]
    public void RpcLoseGame()
    {
        LevelManager.Instance.win = false;
        SceneManager.LoadScene("MainMenu");
    }

    private void ApplyKnockback(Vector3 otherTransformPos)
    {
        Vector3 direction = transform.position - otherTransformPos;
        m_rigidBody.AddForce(direction.normalized * knockbackStrength);
    }

    [Command]
    void CmdShoot(Vector3 direction, Quaternion rotation)
    {
        GameObject bullet = Instantiate(m_bulletPrefab, bulletSpawnPosition.position, rotation);
        NetworkServer.Spawn(bullet);
        PlayerBullet newBullet = bullet.GetComponent<PlayerBullet>();
        newBullet.SetInitialDirection(direction);
//        foreach (Transform t in GetComponentsInChildren<Transform>())
//        {
//            Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), t.gameObject.GetComponent<Collider>());
//        }
        newBullet.transform.position = bulletSpawnPosition.position;
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
        if (m_health + value < MAX_HEALTH)
        {
            m_health += value;
        }
        else
        {
            m_health = MAX_HEALTH;
        }
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