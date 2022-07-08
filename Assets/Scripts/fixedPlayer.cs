using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixedPlayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.collider is CapsuleCollider2D)
            collision.transform.SetParent(gameObject.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.collider is CapsuleCollider2D)
            collision.transform.SetParent(null);
    }
}
