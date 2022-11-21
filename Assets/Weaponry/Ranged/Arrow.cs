using System;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using Weaponry.Ranged;

public class Arrow : MonoBehaviour
{
    [HideInInspector] public float ArrowVelocity;
    [SerializeField] private Rigidbody2D _rb;
    
    private void FixedUpdate()
    {
        _rb.velocity = transform.up * ArrowVelocity;
    }
}
