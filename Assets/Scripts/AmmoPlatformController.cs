using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPlatformController : MonoBehaviour {

	public static AmmoPlatformController instance;
	public bool ammoCooldown;

    [SerializeField]
    private TowerController m_towerController;

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
            //			if (!GameManager.Instance.ammoCooldown) {
            //				GameManager.Instance.ammoCooldown = true;
            //				HUDController.instance.ammo = HUDController.instance.maxAmmo;
            //				HUDController.instance.ammoText.text = HUDController.instance.maxAmmo + "/" + HUDController.instance.maxAmmo;
            m_towerController.InitiateCooldown();
			Debug.Log("Player came to reload.");
			//}

		}
	}
}
