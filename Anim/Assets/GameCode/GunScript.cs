using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] private AnimationClip _animation;
    
    private Animator _animator;
    private AnimatorOverrideController _override;
    private int _move = Animator.StringToHash("Move");

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _override = new AnimatorOverrideController(_animator.runtimeAnimatorController)
        {
            ["Move"] = _animation
        };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _animator.SetTrigger(_move);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            _animator.runtimeAnimatorController = _override;
        }
            
    }
}
