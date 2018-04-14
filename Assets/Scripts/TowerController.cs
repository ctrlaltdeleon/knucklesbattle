using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerController : MonoBehaviour
{
	//public static TowerController instance;
	public int health = 100; //health points
	private int maxHealth = 100;
	
	private int knucklesDamage; //placebo variable for knuckles damage value from Knuckles Controller


	private Slider towerHealthBarSlider; 

	private Text towerHealthText;


	//HUDController HUD = HUDController.instance.towerHealthBarSlider;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "KnucklesTest")
		{
			health -= 1;
			
			//Debug.Log(HUDtowerHealthBarSlider);
			towerHealthBarSlider = HUDController.instance.towerHealthBarSlider;
			towerHealthText = HUDController.instance.towerHealthText;
			
			HUDController.instance.LoseHealth(health, towerHealthBarSlider, towerHealthText, maxHealth);
			Debug.Log("Knuckle collided with tower damaging it");
			//HP -= knucklesDamage;
		}	
	}

	
}
