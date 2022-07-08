using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Transform startPoint;
    Transform endPoint;
    Transform platform;
    [SerializeField] bool going;
    [SerializeField] float speed;
    [SerializeField] float stopTime;
    void Start()
    {
        platform = gameObject.transform.GetChild(0);
        startPoint = gameObject.transform.GetChild(1);
        endPoint = gameObject.transform.GetChild(2);
        going = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(going)
        {
            platform.position = Vector2.MoveTowards(platform.position, endPoint.position, speed * Time.deltaTime);
            if (platform.position == endPoint.position)
                StartCoroutine("stopHandler", false);
        }
        else
        {
            platform.position = Vector2.MoveTowards(platform.position, startPoint.position, speed * Time.deltaTime);
            if (platform.position == startPoint.position)
                StartCoroutine("stopHandler", true);
        }
    }

    IEnumerator stopHandler(bool go)
    {
        yield return new WaitForSeconds(stopTime);
        going = go;
    }
}
