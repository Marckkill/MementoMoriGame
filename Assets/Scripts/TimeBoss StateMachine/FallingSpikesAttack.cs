using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikesAttack : StateMachineBehaviour
{
    [SerializeField] GameObject spikePrefab;
    [SerializeField] GameObject spikeWarningPrefab;
    private Transform spikeSpawnPoint;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spikeSpawnPoint = GameObject.Find("SpikeSpawnPoint").GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            GameObject spike = Instantiate(spikePrefab,spikeSpawnPoint);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
