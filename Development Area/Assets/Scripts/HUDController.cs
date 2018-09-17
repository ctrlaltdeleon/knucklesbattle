using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Image m_playerHealthIndicator;

    [SerializeField] private Image m_ammoIndicator;

    [SerializeField] private Image m_towerIndicator;

    [SerializeField] private Image cooldownImageAlpha;

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

//    [SerializeField] private PlayerControl PlayerControl.Instance;

    [SerializeField] private TowerController m_towerController;

    void Awake()
    {
    }


    // Use this for initialization
    void Start()
    {
        cooldownImageAlpha.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerControl.Instance == null)
        {
            return;
        }

        //		
        //			
        float playerHealthRatio = (float) PlayerControl.Instance.Health / (float) PlayerControl.Instance.MaxHealth;
        float playerAmmoRatio = (float) PlayerControl.Instance.AmmoCount / (float) PlayerControl.Instance.MaxAmmo;
        float playerAmmo = PlayerControl.Instance.AmmoCount;

        float towerHealthRatio = (float) m_towerController.Health / (float) m_towerController.MaxHealth;

        m_playerHealthIndicator.fillAmount = playerHealthRatio;
        m_ammoIndicator.fillAmount = playerAmmoRatio;
        m_towerIndicator.fillAmount = towerHealthRatio;

        ammoText.text = (PlayerControl.Instance.AmmoCount).ToString() + " / " +
                        (PlayerControl.Instance.MaxAmmo).ToString();
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