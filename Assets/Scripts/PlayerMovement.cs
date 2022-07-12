using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRB;
    CapsuleCollider2D playerFeetCollider;
    BoxCollider2D playerBodyCollider;
    BoxCollider2D wallJumpColl;
    Animator playerAnim;

    [SerializeField] private PhysicsMaterial2D[] playerMaterials = new PhysicsMaterial2D[2]; //primeiro material = padrão, segundo material = tem fricção
    [SerializeField] float walkSpd = 5;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float dashSpeed = 15;
    [SerializeField] private float dashTime = 1f;
    [SerializeField] private float dashCD;
    [SerializeField] private float wallSlideSpeed;
    [SerializeField] private float wallJumpForce;
    [SerializeField] private Vector2 wallJumpDirection;
    public bool canMove = true;
    public bool holdingBlock = false;
    private float lastDash;
    private bool isGrounded;
    private bool isCrouching;
    private bool touchingWall;
    private bool wallSliding;

    //combat tests
    private PlayerCombat combatScript;
    //public bool takingDamage;
    //public bool dead;
    public bool dashing;


    //tests
    [HideInInspector] public Vector2 forceHandler;
    protected virtual void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        playerFeetCollider = gameObject.GetComponent<CapsuleCollider2D>();
        playerBodyCollider = gameObject.GetComponent<BoxCollider2D>();
        wallJumpColl = gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
        playerAnim = gameObject.GetComponent<Animator>();


        combatScript = gameObject.GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        checkWallAndGround();
        if (!canMoveCheck())
            return;
        jumpAnim();
        jump();

    }
    protected virtual void FixedUpdate()
    {
        if (!canMoveCheck())
            return;
        invertSprite();
        walk();
        dash();
        crouch();
        slopeCheck();    
    }

    private void walk()
    {
        float moveSpeed = Input.GetAxis("Horizontal") * walkSpd;
        bool playerMoving = Mathf.Abs(playerRB.velocity.x) > 0.1f;

        if(!isCrouching && !dashing)
        {
            playerRB.velocity = new Vector2(moveSpeed, playerRB.velocity.y);
            checkForceApplied();
        }

        if (wallSliding)
        {
            if(playerRB.velocity.y < -wallSlideSpeed)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, -wallSlideSpeed);
                
            }
        }
        playerAnim.SetBool("Running", playerMoving);
    }

    void jump()
    {
        if(isCrouching || holdingBlock)
            return;

        if (Input.GetButtonDown("Jump") && isGrounded && !wallSliding)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x * Time.deltaTime, jumpForce);
        }
        else if (touchingWall && Input.GetButtonDown("Jump")) // wallsliding
        {
            Vector2 force = new Vector2(wallJumpForce * wallJumpDirection.x * -gameObject.transform.localScale.x, wallJumpForce * wallJumpDirection.y);

            playerRB.velocity = Vector2.zero;

            playerRB.AddForce(force, ForceMode2D.Impulse);

            jumpAnim();

            StartCoroutine("StopMove");
        }
    }
    IEnumerator StopMove()
    {
        canMove = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

        yield return new WaitForSeconds(.3f);

        canMove = true;
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
        if (dashing || holdingBlock)
            return;

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
        if (holdingBlock)
            return;

        if (Input.GetAxisRaw("Vertical") < 0 && isGrounded)
        {
            isCrouching = true;
            playerRB.velocity = Vector2.zero;
            playerBodyCollider.enabled = false;
        }
        else
        {
            isCrouching = false;
            playerBodyCollider.enabled = true;
        }


        playerAnim.SetBool("Crouching", isCrouching);
    }

    void dash()
    {
        if (wallSliding || holdingBlock)
            return;

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
        if (combatScript.takingDamage || combatScript.dead || !canMove)
            return false;
        else
            return true;
    }

    private void checkWallAndGround()
    {
        if (playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Slope")) || playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Wall")))
            isGrounded = true;
        else
            isGrounded = false;

        touchingWall = wallJumpColl.IsTouchingLayers(LayerMask.GetMask("Wall")) || wallJumpColl.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (touchingWall && !isGrounded && playerRB.velocity.y < 0 && Input.GetAxisRaw("Horizontal") != 0)
        {
            wallSliding = true;
            playerAnim.SetBool("WallSlide", wallSliding);
        }
        else
        {
            wallSliding = false;
            playerAnim.SetBool("WallSlide", wallSliding);
        }
    }

    private void checkForceApplied()
    {
        if(forceHandler != Vector2.zero)
        {
            playerRB.AddForce(forceHandler, ForceMode2D.Force); // serve pra adicionar efeitos tipo vento e os krl
        }
    }
    private void jumpAnim()
    {
       bool goingUp;

        playerAnim.SetBool("Jump", !isGrounded);

        if (playerRB.velocity.y > Mathf.Epsilon)
           goingUp = true;
       else
           goingUp = false;

       playerAnim.SetBool("GoingUp", goingUp);
       playerAnim.SetBool("GoingDown", !goingUp);
    }

}
