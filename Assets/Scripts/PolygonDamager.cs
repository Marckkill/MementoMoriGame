using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonDamager : Damager
{
    private PolygonCollider2D coll;
    protected override void Start()
    {
        coll = gameObject.GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        coll.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);

            //clear the array
            hits[i] = null;
        }
    }
}
