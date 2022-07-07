using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBossIdle : StateMachineBehaviour
{
    private int attackChosen;
    private int previousAttack = 9999;
    [SerializeField] float idleTime = 3f;
    private float startTime;
    private bool doOnce;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        startTime = Time.time;
        doOnce = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Time.time > startTime + idleTime)
        {
            if(doOnce)
            {
                doOnce = false;
                attackChosen = Random.Range(0, 2);
                if (attackChosen == previousAttack)
                {
                    while (attackChosen == previousAttack)
                        attackChosen = Random.Range(0, 2);
                }
                previousAttack = attackChosen;
                switch (attackChosen)
                {
                    case 0:
                        animator.SetTrigger("Waves");
                        break;
                    case 1:
                        animator.SetTrigger("SandStorm");
                        break;
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
