using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingBlock : MonoBehaviour
{
    private bool beingHeld;
    private Rigidbody2D rb;
    private Rigidbody2D playerRB;
    private FixedJoint2D fixedJoint;
    private BoxCollider2D playerColl;
    private BoxCollider2D boxCollider;
    private PlayerMovement moveScript;


    private void Start()
    {
        fixedJoint = gameObject.GetComponent<FixedJoint2D>();
        moveScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerRB = moveScript.gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        beingHeld = false;
        playerColl = GameObject.FindGameObjectWithTag("WallJumpColl").GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        onHeld();
    }
    private void onHeld()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(playerColl.IsTouching(boxCollider) && !beingHeld)
            {
                rb.mass = 50;
                fixedJoint.enabled = true;
                fixedJoint.connectedBody = playerRB;
                transform.SetParent(playerColl.transform);
                beingHeld = true;
            }
            else
            {
                rb.mass = 5000;
                fixedJoint.enabled = false;
                transform.SetParent(null);
                beingHeld = false;
            }
            moveScript.holdingBlock = beingHeld;
        }

    }
}
