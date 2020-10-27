using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestScript : MonoBehaviour
{
    private Animator _animator;
    private bool _crouch;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        _animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        if (Input.GetKey(KeyCode.C))
            _crouch = !_crouch;
        _animator.SetBool("Crouch", _crouch);
    }
}
