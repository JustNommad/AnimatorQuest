using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    [SerializeField] private float _windForce = 100.0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        other.attachedRigidbody.AddForce(Vector2.up * _windForce);
    }
}
