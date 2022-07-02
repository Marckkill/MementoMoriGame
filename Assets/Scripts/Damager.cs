using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : Collideable
{
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Player")
            playerDamage(coll);
    }

    private void playerDamage(Collider2D playerColl)
    {
        playerColl.GetComponent<PlayerCombat>().dmgPlayer(gameObject.transform.position.x, 1f); // o dano é só um por agr pq isso não importa nesse estágio    
    }


}
