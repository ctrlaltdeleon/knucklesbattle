using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
	public static HUDController instance;
	

	public Slider playerHealthBarSlider;
	public Slider towerHealthBarSlider;

	public Image cooldownImageAlpha;
	
	public Text playerHealthText;
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
	
	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
		
	}
	
	
	// Use this for initialization
	void Start ()
	{
		cooldownImageAlpha.enabled = false;
		playerHealthBarSlider.minValue = playerMinHealth;
		playerHealthBarSlider.maxValue = playerMaxHealth;
		playerHealthText.text = playerMaxHealth + "/" + playerMaxHealth;
		
		towerHealthBarSlider.minValue = towerMinHealth;
		towerHealthBarSlider.maxValue = towerMaxHealth;
		towerHealthText.text = towerMaxHealth + "/" + towerMaxHealth;
		
	}
	
	// Update is called once per frame
	void Update () {

//		if (GameManager.Instance.ammoCooldown == true)
//		{
//			cooldownImageAlpha.enabled = true;
//			cooldownImageAlpha.fillAmount -= 1.0f / waitTime * Time.deltaTime;
//			if (cooldownImageAlpha.fillAmount == 0) {
//				GameManager.Instance.ammoCooldown = false;
//				cooldownImageAlpha.enabled = false;
//				cooldownImageAlpha.fillAmount = 1;
//			}
//		}
//			
		
		if (Input.GetKeyDown(KeyCode.Space))
		{
			//LosePlayerHealth(10);
			if (playerHealth > 0)
			{
				playerHealth -= 10;
				LoseHealth(playerHealth, playerHealthBarSlider, playerHealthText, playerMaxHealth);
			}
		}

		if (Input.GetKey(KeyCode.S))
		{
			Fire();
		}
		
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
