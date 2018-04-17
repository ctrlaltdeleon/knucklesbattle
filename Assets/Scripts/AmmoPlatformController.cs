using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPlatformController : MonoBehaviour {

	public static AmmoPlatformController instance;
	private bool ammoCooldown = false;

	[SerializeField] 
	public bool AmmoCooldown {get { return ammoCooldown;} set { ammoCooldown = value;}}

    [SerializeField]
    public TowerController m_towerController;

	[SerializeField]
	private PlayerControl m_playerControl;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "PlayerEntity")
		{
			if (ammoCooldown == false)
			{
				m_playerControl.AmmoCount = m_playerControl.MaxAmmo;
			}
			ammoCooldown = true;
            //m_towerController.InitiateCooldown();
			Debug.Log("Player came to reload.");
		}
	}
}
