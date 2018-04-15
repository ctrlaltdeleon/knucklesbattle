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

    public int Health { get { return health; } }
    public int MaxHealth { get { return maxHealth; } }


	//HUDController HUD = HUDController.instance.towerHealthBarSlider;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Initiates the cooldown.
    /// </summary>
    public void InitiateCooldown()
    {
        //TODO Implement cooldown code.
    }

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "KnucklesTest")
		{
			health -= 1;
			
			//Debug.Log(HUDtowerHealthBarSlider);
			
			Debug.Log("Knuckle collided with tower damaging it");
			//HP -= knucklesDamage;
		}	
	}

	
}
