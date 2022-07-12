using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform[] anchorPoints;
    Transform platform;
    bool going;
    private int anchorGoingTo;
    [SerializeField] float speed;
    [SerializeField] float interval;
    private bool doOnce;
    void Start()
    {
        platform = gameObject.transform.GetChild(0);
        for (int i = 0; i < anchorPoints.Length; i++)
        {
            anchorPoints[i] = gameObject.transform.GetChild(i + 1);
        }
        going = true;
        anchorGoingTo = 1;
        doOnce = true;
    }

    void Update()
    {


        if(going)
        {
            platform.position = Vector2.MoveTowards(platform.position, anchorPoints[anchorGoingTo].position, speed * Time.deltaTime);
            if (platform.position == anchorPoints[anchorGoingTo].position && doOnce)
                StartCoroutine("stopHandler", false);
        }
        else
        {
            platform.position = Vector2.MoveTowards(platform.position, anchorPoints[anchorGoingTo].position, speed * Time.deltaTime);
            if (platform.position == anchorPoints[anchorGoingTo].position && doOnce)
                StartCoroutine("stopHandler", true);
        }  
    }

    IEnumerator stopHandler(bool go)
    {
        doOnce = false;
        yield return new WaitForSeconds(interval);
        if(!go)
        {
            if (anchorGoingTo == anchorPoints.Length - 1)
            {
                going = false;
                anchorGoingTo--;
            } 
            else if (anchorGoingTo < anchorPoints.Length)
                anchorGoingTo++;
        }
        else
        {
            if (anchorGoingTo == 0)
            {
                going = true;
                anchorGoingTo++;
            }
            else
                anchorGoingTo--;
        }

        doOnce = true;
    } 
}