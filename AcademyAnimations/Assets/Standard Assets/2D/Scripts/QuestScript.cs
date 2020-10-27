using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestScript : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        _animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        _animator.SetBool("Crouch", Input.GetKey(KeyCode.C));
    }
}
