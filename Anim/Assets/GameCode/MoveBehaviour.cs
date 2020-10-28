using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : StateMachineBehaviour
{
    [SerializeField] private float _timer;

    private int _stop = Animator.StringToHash("Stop");
    private float _time = 0.0f;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var index = animator.GetLayerIndex("ScaleLayer");
        animator.SetLayerWeight(index, 1.0f);
        _time = 0.0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _time += Time.deltaTime;
        Debug.Log("Update");
        if (_time >= _timer)
        {
            var index = animator.GetLayerIndex("ScaleLayer");
            animator.SetLayerWeight(index, 0.0f);
            animator.SetTrigger(_stop);
            _time = 0.0f;
        }
    }
}
