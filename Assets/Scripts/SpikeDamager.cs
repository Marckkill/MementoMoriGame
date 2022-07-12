using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpikeDamager : Damager
{
    protected override void Start()
    {
        boxCollider = gameObject.GetComponent<TilemapCollider2D>();
    }
}
