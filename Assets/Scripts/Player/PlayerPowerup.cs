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

    [SerializeField]
    private PowerupType m_powerupType;
    [SerializeField]
    private int m_rewardValue;

    public PowerupType PowerUp { get { return m_powerupType; } }

	// Use this for initialization
	void Start () {
        m_powerupType = PowerupType.HEALTH;
        m_rewardValue = 100;
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
        float rotateValue = 0.55f * Time.deltaTime;
        transform.Rotate(0f, rotateValue, 0f);
	}

    //Reward player with things. 
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Should be occuring.");
        if (collision.gameObject && collision.gameObject.tag == "PlayerEntity")
        {
            var PlayerHandle = collision.gameObject.GetComponent<PlayerControl>();
            switch (m_powerupType)
            {
                case PowerupType.HEALTH:
                    PlayerHandle.RewardHealth(m_rewardValue);
                    break;
                case PowerupType.MESEEKS:
                    PlayerHandle.RewardMeseeks();
                    break;
                case PowerupType.BULLETS:
                    PlayerHandle.RewardAmmo(m_rewardValue);
                    break;
                case PowerupType.SPECIAL_WEAPON:
                    PlayerHandle.RewardSpecialWeapon();
                    break;
            }
        }
        Destroy(this.gameObject);
    }
}
