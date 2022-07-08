using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Damager
{
    protected override void OnCollide(Collider2D coll)
    {
        base.OnCollide(coll);

        if(coll.tag == "Player" || coll.tag == "Wall" || coll.tag == "Ground")
        Destroy(gameObject);
    }
}
