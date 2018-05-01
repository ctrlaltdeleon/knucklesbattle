using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{

	public Text levelText;

	// Use this for initialization
	void Start ()
	{
		levelText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		levelText.text = "Level " + GameManager.Instance.level.ToString();
	}
}
