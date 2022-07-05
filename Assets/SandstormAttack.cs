using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandstormAttack : StateMachineBehaviour
{
    [SerializeField] Vector2 windForce;
    private PlayerMovement movementScript;
    [SerializeField] float windDuration;
    private float windStart;
    private bool doOnce;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        windStart = Time.time;
        doOnce = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Time.time > windStart + windDuration)
        {
            if(doOnce)
            {
                doOnce = false;
                movementScript.forceHandler = Vector2.zero;
                animator.SetTrigger("Idle");
            }
        }
        else
        {
            movementScript.forceHandler = windForce;
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
