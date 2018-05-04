using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFX : MonoBehaviour {

    private bool m_getDamage = false;
    private bool m_damageActionRun = false;

    private bool m_isShaking = false;
    private float m_shakeDuration = 0f;
    private float shakeAmount = 0.7f;
    private float decreaseFactor = 1.0f;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        if (m_shakeDuration > 0)
        {

        }
        else {
            m_shakeDuration = 0.0f;

        }
    }

    public void RunRedScreen() {
        m_getDamage = true;
        m_damageActionRun = true;
    }

    public void RunTowerShake() {
        m_shakeDuration = 0.1f;
    }
}
