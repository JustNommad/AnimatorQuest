using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;

public class Enemy : MonoBehaviour
{
    private PlatformerCharacter2D m_Character;
    private Rigidbody2D _rigidbody;
    private Vector2 _angularDir;
    private Vector2 _frontDir;
    private float _dir = 1.0f;

    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacter2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _angularDir = new Vector2(1.0f, -1.0f);
        _frontDir = new Vector2(1.0f, 0.0f);
    }

    private void FixedUpdate()
    {
        m_Character.Move(_dir / 2, false, false);
        
        var front = 
            Physics2D.Raycast(transform.position, _frontDir, 0.5f);
        var angular = 
            Physics2D.Raycast(transform.position,_angularDir, 2.0f);
        
        if (!angular || front)
        {
            _dir *= -1;
            _frontDir.x = _angularDir.x = _dir;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Fire"))
        {
            var hit = other.contacts[0].point;
            var pos = new Vector2(transform.position.x,transform.position.y);
            var dir = (hit - pos).normalized * 10.0f;
            _dir = 0;
            _rigidbody.freezeRotation = false;
            _rigidbody.AddForceAtPosition(dir, hit,ForceMode2D.Impulse);
        }
    }
}
