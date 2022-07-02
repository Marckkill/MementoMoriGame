using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRB;
    CapsuleCollider2D playerFeetCollider;
    BoxCollider2D playerBodyCollider;
    Animator playerAnim;

    [SerializeField] private PhysicsMaterial2D[] playerMaterials = new PhysicsMaterial2D[2]; //primeiro material = padrão, segundo material = tem fricção
    [SerializeField] private float walkSpd = 5;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float dashSpeed = 15;
    [SerializeField] private float dashTime = 1f;
    [SerializeField] private float dashCD;
    private float lastDash;
    private bool isGrounded;
    private bool isCrouching;

    //combat tests
    public bool takingDamage;
    public bool dead;
    public bool dashing;
    protected virtual void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        playerFeetCollider = gameObject.GetComponent<CapsuleCollider2D>();
        playerBodyCollider = gameObject.GetComponent<BoxCollider2D>();
        playerAnim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!canMoveCheck())
            return;
        jump();
    }
    protected virtual void FixedUpdate()
    {
        if (!canMoveCheck())
            return;
        invertSprite();
        walk();
        dash();
        //jump();
        crouch();
        slopeCheck();    
    }

    private void walk()
    {
        float moveSpeed = Input.GetAxis("Horizontal") * walkSpd;
        bool playerMoving = Mathf.Abs(playerRB.velocity.x) > 0.1f;

        if(!isCrouching)
            playerRB.velocity = new Vector2(moveSpeed, playerRB.velocity.y);

        playerAnim.SetBool("Running", playerMoving);
    }

    void jump()
    {
        if (playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Slope")))
            isGrounded = true;
        else
            isGrounded = false;

        if(isCrouching)
            return;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x * Time.deltaTime, jumpForce);
        }

        playerAnim.SetBool("Jump", !isGrounded);

        //animação boba
        bool goingUp;
        if (playerRB.velocity.y > Mathf.Epsilon)
            goingUp = true;
        else
            goingUp = false;

        playerAnim.SetBool("GoingUp", goingUp);
        playerAnim.SetBool("GoingDown", !goingUp);

    }

    void slopeCheck()
    {
        if(playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Slope")))
        {
            playerRB.sharedMaterial = playerMaterials[1];
        }
        else
        {
            playerRB.sharedMaterial = playerMaterials[0];
        }
    }

    void invertSprite()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        else if (Input.GetAxisRaw("Horizontal") < 0)
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);

       /* bool player_has_horizontal_speed = Mathf.Abs(playerRB.velocity.x) > Mathf.Epsilon;

        if(player_has_horizontal_speed)
            transform.localScale = new Vector2(Mathf.Sign(playerRB.velocity.x) * 5f, transform.localScale.y);*/
    }

    void crouch()
    {
        if (Input.GetAxisRaw("Vertical") < 0 && isGrounded)
        {
            isCrouching = true;
            playerRB.velocity = Vector2.zero;
        }
        else
            isCrouching = false;

        playerAnim.SetBool("Crouching", isCrouching);
    }

    void dash()
    {
        if(Input.GetButton("Fire3") && Time.time > dashCD + lastDash)
            StartCoroutine("dashHandler");
    }

    IEnumerator dashHandler()
    {
        float playerFacing = gameObject.transform.localScale.x;
        lastDash = Time.time;
        dashing = true;
        playerRB.gravityScale = 0;
        playerAnim.SetBool("Dash", dashing);
        playerRB.velocity = new Vector2(dashSpeed * Mathf.Sign(playerFacing) * Time.deltaTime, 0);
        yield return new WaitForSeconds(dashTime);
        dashing = false;
        playerRB.gravityScale = 5;
        playerAnim.SetBool("Dash", dashing);
    }

    private bool canMoveCheck()
    {
        if (takingDamage || dead || dashing)
            return false;
        else
            return true;
    }

}
