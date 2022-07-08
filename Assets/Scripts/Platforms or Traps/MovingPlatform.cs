using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform[] anchorPoints;
    Transform startPoint;
    Transform endPoint;
    Transform platform;
    [SerializeField] bool going;
    private int anchorGoingTo;
    [SerializeField] float speed;
    [SerializeField] float interval;
    void Start()
    {
        platform = gameObject.transform.GetChild(0);
        for (int i = 0; i < anchorPoints.Length; i++)
        {
            anchorPoints[i] = gameObject.transform.GetChild(i + 1);
        }
        going = true;
        anchorGoingTo = 1;
    }

   /* void Update()
    {

        if(going)
        {
            platform.position = Vector2.MoveTowards(platform.position, anchorPoints[anchorGoingTo].position, speed * Time.deltaTime);
            if (platform.position == anchorPoints[anchorGoingTo].position)
                StartCoroutine("stopHandler", false);
        }
        else
        {
            platform.position = Vector2.MoveTowards(platform.position, anchorPoints[anchorGoingTo].position, speed * Time.deltaTime);
            if (platform.position == anchorPoints[anchorGoingTo].position)
                StartCoroutine("stopHandler", true);
        }
    }

    IEnumerator stopHandler(bool go)
    {
        yield return new WaitForSeconds(interval);
        if(!go)
        {
            if (anchorPoints.Length == anchorGoingTo)
                going = false;
            else
                anchorGoingTo++;
        }
        else
        {
            if (anchorGoingTo == 0)
                going = true;
            else
                anchorGoingTo--;
        }
    } */
}