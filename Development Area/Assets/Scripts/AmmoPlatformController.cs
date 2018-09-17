using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPlatformController : MonoBehaviour
{
    public static AmmoPlatformController instance;
    private bool ammoCooldown = false;

    [SerializeField]
    public bool AmmoCooldown
    {
        get { return ammoCooldown; }
        set { ammoCooldown = value; }
    }

    [SerializeField] public TowerController m_towerController;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerEntity")
        {
            if (ammoCooldown == false)
            {
                PlayerControl.Instance.AmmoCount = PlayerControl.Instance.MaxAmmo;
            }

            ammoCooldown = true;
        }
    }
}