using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPlatformConroller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			HUDController.instance.ammo = HUDController.instance.maxAmmo;
			HUDController.instance.ammoText.text = HUDController.instance.maxAmmo + "/" + HUDController.instance.maxAmmo;
			Debug.Log("Player came to reload.");
		}
	}
}
