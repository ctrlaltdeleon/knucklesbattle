using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    private Image m_playerHealthIndicator;

    [SerializeField]
    private Image m_ammoIndicator;

    [SerializeField]
    private Image m_towerIndicator;

    [SerializeField]
	private Image cooldownImageAlpha;
	
	public Text ammoText;
	public Text towerHealthText;
	
	private int playerHealth = 100;
	private int playerMaxHealth = 100;
	private int playerMinHealth = 0;
	
	public int towerHealth = 100;
	public int towerMaxHealth = 100;
	private int towerMinHealth = 0;

	public int maxAmmo = 1000;
	public int ammo = 1000;

	public bool ammoCooldown = false;

	public float waitTime = 5.0f;

    [SerializeField]
    private PlayerControl m_playerControl;

    [SerializeField]
    private TowerController m_towerController;
	
	void Awake () {

	}
	
	
	// Use this for initialization
	void Start ()
	{
		cooldownImageAlpha.enabled = false;
		
	}
	
	// Update is called once per frame
	void Update () {

        //		
        //			
        float playerHealthRatio = (float)m_playerControl.Health / (float)m_playerControl.MaxHealth;
        float playerAmmoRatio = (float)m_playerControl.AmmoCount / (float)m_playerControl.MaxAmmo;
        float playerAmmo = m_playerControl.AmmoCount;

        float towerHealthRatio = (float)m_towerController.Health / (float)m_towerController.MaxHealth;

        m_playerHealthIndicator.fillAmount = playerHealthRatio;
        m_ammoIndicator.fillAmount = playerAmmoRatio;
        m_towerIndicator.fillAmount = towerHealthRatio;

        ammoText.text = (m_playerControl.AmmoCount).ToString() + " / " + (m_playerControl.MaxAmmo).ToString();
	}

	private void Fire()
	{
		if (ammo > 0)
		{
			ammo -= 1;
			ammoText.text = ammo + "/" + maxAmmo;
		}
	}

	public void LoseHealth(int health, Slider healthBarSlider, Text healthText, int maxHealth)
	{
		healthBarSlider.value = health;
		healthText.text = health + "/" + maxHealth;
		Debug.Log("Health: " + health);
	}

}
