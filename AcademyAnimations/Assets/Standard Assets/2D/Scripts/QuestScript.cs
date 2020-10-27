using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestScript : MonoBehaviour
{
    private Animator _animator;
    private int _speedTrigger = Animator.StringToHash("Speed");
    private int _crouchTrigger = Animator.StringToHash("Crouch");
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        _animator.SetFloat(_speedTrigger, Mathf.Abs(Input.GetAxis("Horizontal")));
        _animator.SetBool(_crouchTrigger, Input.GetKey(KeyCode.C));
    }
}
