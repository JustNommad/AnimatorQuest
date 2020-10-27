using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class ButtonController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ParticleSystem _particleSystem;
    
    private int _rotateAnim = Animator.StringToHash("Rotate");
    private int _colorAnim = Animator.StringToHash("Color");
    private int _jumpAnim = Animator.StringToHash("Jump");
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _particleSystem.Stop();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _particleSystem.Play();
        _animator.SetFloat(_jumpAnim, Random.Range(0.0f, 1.0f));
        _animator.SetFloat(_colorAnim, Random.Range(0.0f, 1.0f));
        _animator.SetFloat(_rotateAnim, Random.Range(0.0f, 1.0f));
    }
}
