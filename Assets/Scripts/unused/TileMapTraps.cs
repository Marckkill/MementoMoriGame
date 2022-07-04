using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapTraps : Damager
{
    private Collider2D tilemapColl;
    protected override void Start()
    {
        tilemapColl = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        tilemapColl.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);

            //clear the array
            hits[i] = null;
        }
    }

    protected override void playerDamage(Collider2D playerColl)
    {
        Tilemap tilemap = gameObject.GetComponent<Tilemap>();
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
        Debug.Log(cellPosition.x);
        //transform.position = tilemap.GetCellCenterWorld(cellPosition);
        playerColl.GetComponent<PlayerCombat>().dmgPlayer(cellPosition.x, 1f); // o dano é só um por agr pq isso não importa nesse estágio   
    }
}
