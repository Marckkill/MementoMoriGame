using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    BoxCollider2D leverCollider;
    BoxCollider2D elevatorColl;
    BoxCollider2D playerColl;
    Transform startPos;
    Transform endPos;
    Transform elevator;
    [SerializeField] float speed;
    [SerializeField] bool goingUp;
    
    void Start()
    {
        leverCollider = transform.GetChild(1).GetComponent<BoxCollider2D>();
        elevatorColl = transform.GetChild(0).GetComponent<BoxCollider2D>();
        playerColl = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        startPos = transform.GetChild(2);
        endPos = transform.GetChild(3);
        elevator = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerColl.IsTouching(elevatorColl))
        {

                if (goingUp)
                    elevator.position = Vector2.MoveTowards(elevator.position, endPos.position, speed * Time.deltaTime);
                else
                    elevator.position = Vector2.MoveTowards(elevator.position, startPos.position, speed * Time.deltaTime);

        }

        if(!playerColl.IsTouching(elevatorColl))
        {
            if (Mathf.Abs(elevator.position.y - endPos.position.y) < 0.2f)
                goingUp = false;
            if (Mathf.Abs(elevator.position.y - startPos.position.y) < 0.2f)
                goingUp = true;
        }

        
    }
}
