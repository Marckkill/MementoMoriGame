using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private SessionManager sessionManager;
    private Rigidbody2D playerRB;
    private PlayerMovement movementScript;
    private Animator playerAnim;
    public Vector2 knockback;
    [SerializeField]private float knockbackTime = 1f;

    private float immuneTime = 5f; // 5 = 1 sec
    public bool immune;

    private SpriteRenderer playerSprite;
    private void Start()
    {
        sessionManager = GameObject.FindGameObjectWithTag("SessionManager").GetComponent<SessionManager>();
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        movementScript = gameObject.GetComponent<PlayerMovement>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        playerAnim = gameObject.GetComponent<Animator>();
    }
    public void dmgPlayer(float enemyPosX, float dmg)
    {
        if (immune)
            return;

        sessionManager.loseHealth(dmg);
        if (sessionManager.playerHealth <= 0) // morte
            playerDeath(enemyPosX);
        else
        {
            StartCoroutine("knockBackHandler", enemyPosX);
            StartCoroutine("ImmunityFrames");
        }
        

    }
    IEnumerator knockBackHandler(float enemyPosX)
    {
        movementScript.takingDamage = true;
        playerAnim.SetBool("Hurt", movementScript.takingDamage);
        playerRB.velocity = new Vector2(knockback.x * -Mathf.Sign(gameObject.transform.position.x - enemyPosX), knockback.y);
        yield return new WaitForSeconds (knockbackTime);
        movementScript.takingDamage = false;
        playerAnim.SetBool("Hurt", movementScript.takingDamage);

        if (movementScript.dead)
            playerRB.velocity = new Vector2(0, 0);
    }

    IEnumerator ImmunityFrames()
    {
        immune = true;
        for (int i = 0; i < immuneTime; i++)
        {
           yield return new WaitForSeconds(0.1f);
           playerSprite.enabled = false;
           yield return new WaitForSeconds(0.1f);
           playerSprite.enabled = true;
        }
        immune = false;
    }

    private void playerDeath(float enemyPosX)
    {
        movementScript.dead = true;
        playerAnim.SetBool("Death", true);
        StartCoroutine("knockBackHandler", enemyPosX);
    }
}
