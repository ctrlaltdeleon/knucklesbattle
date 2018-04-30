using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitController : MonoBehaviour
{
    [SerializeField] public KnucklesController m_KnucklesController;
    private Rigidbody Rb;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Rb.velocity);
    }
}