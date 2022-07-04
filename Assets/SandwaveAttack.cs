using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwaveAttack : StateMachineBehaviour
{
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public GameObject sandWavePrefab;
    public Transform sandWaveSpawnPoint;
    public Transform sandWaveDespawnPoint;
    public float waveVelocity;

    public float interval;
    private float intervalStart;
    private GameObject[] sandwave = new GameObject[10];
    public int sandWaveCount;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sandWaveSpawnPoint = GameObject.Find("SandWaveSpawnPoint").GetComponent<Transform>();
        sandWaveDespawnPoint = GameObject.Find("SandWaveDespawnPoint").GetComponent<Transform>();
        for (int i = 0; i < 10; i++)
        {
            sandwave[i] = null;
        }
        sandWaveCount = 0;
        intervalStart = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log(Time.time);
        if((Time.time > interval + intervalStart) && sandWaveCount < 10 )
        {
            Debug.Log("BATATA");
            intervalStart = Time.time;
            sandwave[sandWaveCount] = Instantiate(sandWavePrefab, sandWaveSpawnPoint);
            sandwave[sandWaveCount].GetComponent<Rigidbody2D>().velocity = new Vector2(waveVelocity * Time.fixedDeltaTime, 0);
            sandWaveCount++;
        }
        else if (sandWaveCount >= 10)
        {
            if(sandwave[sandWaveCount-1].transform.position.x < sandWaveDespawnPoint.position.x)
            {
                for (int i = 0; i < 10; i++)
                {
                    Destroy(sandwave[i]);
                }
                animator.SetTrigger("Idle");
            }
            
        }
    }
    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
