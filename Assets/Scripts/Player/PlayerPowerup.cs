using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds PowerUp Information
/// </summary>
public class PlayerPowerup : MonoBehaviour {

    public enum PowerupType : uint {
        HEALTH = 0,
        MESEEKS = 1,
        BULLETS = 2,
        SPECIAL_WEAPON = 3
    }

    public enum RotateAxis : uint {
        X,
        Y,
        Z
    }

    [SerializeField]
    private PowerupType m_powerupType = PowerupType.HEALTH;
    [SerializeField]
    private int m_rewardValue = 100;

    [SerializeField]
    private RotateAxis m_rotAxis = RotateAxis.Y;

    private Rigidbody m_rigidBody;

    public PowerupType PowerUp { get { return m_powerupType; } }

    [SerializeField]
    private string m_sound;

	// Use this for initialization
	void Start () {
        m_rigidBody = GetComponent<Rigidbody>();
	}

    /// <summary>
    /// Initializes the settings.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="rewardValue">The reward value.</param>
    void InitSettings(PowerupType type, int rewardValue)
    {
        m_powerupType = type;
        m_rewardValue = rewardValue;
    }
	
	// Update is called once per frame
	void Update () {
        float rotateValue = 1.5f * Time.deltaTime;
        switch (m_rotAxis)
        {
            case RotateAxis.X:
                m_rigidBody.AddRelativeTorque(new Vector3(rotateValue, 0.0f, 0.0f));
                break;
            case RotateAxis.Y:
                m_rigidBody.AddRelativeTorque(new Vector3(0.0f, rotateValue, 0.0f));
                break;
            case RotateAxis.Z:
                m_rigidBody.AddRelativeTorque(new Vector3(0.0f, 0.0f, rotateValue));
                break;
        }

	}

    //Reward player with things. 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject && collision.gameObject.tag == "PlayerEntity")
        {
            var PlayerHandle = collision.gameObject.GetComponent<PlayerControl>();
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySound(m_sound);
            }
            switch (m_powerupType)
            {
                case PowerupType.HEALTH:
                    Debug.Log("Health Rewarded!");
                    PlayerHandle.RewardHealth(m_rewardValue);
                    Destroy(this.gameObject);
                    break;
                case PowerupType.MESEEKS:
                    Debug.Log("Awarding player with Meseeks!");
                    PlayerHandle.RewardMeseeks();
                    Destroy(this.gameObject);
                    break;
                case PowerupType.BULLETS:
                    PlayerHandle.RewardAmmo(m_rewardValue);
                    Destroy(this.gameObject);
                    break;
                case PowerupType.SPECIAL_WEAPON:
                    PlayerHandle.RewardSpecialWeapon();
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}
