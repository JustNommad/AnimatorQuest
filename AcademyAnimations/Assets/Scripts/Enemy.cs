using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector3 _defaultPos;
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private Sprite _deadSprite;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private AudioClip _step;

    [SerializeField] private GameObject _targetA;
    [SerializeField] private GameObject _targetB;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;

    private bool isDead;
    private bool isRight;
    private int _health = 3;
    void Start()
    {
        transform.position = _defaultPos;
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if(!isDead)
            CalculateMovement();
    }

    private void CalculateMovement()
    {
        var dir = new Vector3(0, 0,0);
        
        if (isRight)
            dir = new Vector3(1.0f, 0,0);
        else 
            dir = new Vector3(-1.0f, 0,0);
        
        transform.Translate(dir * _speed * Time.deltaTime);
        CheckRight();
    }

    private void CheckRight()
    {
        if (transform.position.x <= _targetA.transform.position.x + 0.5f)
        {
            isRight = !isRight;
            _sprite.flipX = true;
        }
        else if (transform.position.x >= _targetB.transform.position.x - 0.5f)
        {
            isRight = !isRight;
            _sprite.flipX = false;
        }
    }

    public void Damage(Vector3 dir)
    {
        _health--;
        if (_health == 0)
        {
            isDead = true;
            _rigidbody.simulated = false;
            _animator.SetBool("Dead", isDead);
            return;
        }
        _animator.SetBool("Damage", true);
        _rigidbody.AddForce(dir * 2.0f);
    }

    private void EndDamage()
    {
        _animator.SetBool("Damage", false);
    }

    private void Dead()
    {
        _sprite.sprite = _deadSprite;
    }
    private void Step()
    {
        _particle.Play();
        AudioSource.PlayClipAtPoint(_step, transform.position);
    }
}
