using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScreenController : MonoBehaviour {

    public static DamageScreenController Instance = null;
    [SerializeField]
    private float damageTimeout;
    [SerializeField]
    private GameObject damagePanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        damagePanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (damageTimeout > 0.0f)
        {
            damagePanel.SetActive(true);
            damageTimeout -= 0.4f * Time.deltaTime;
        }
        else {
            damageTimeout = 0.0f;
            damagePanel.SetActive(false);
        }
	}

    public void Run()
    {
        damageTimeout = 0.15f;
    }
}
