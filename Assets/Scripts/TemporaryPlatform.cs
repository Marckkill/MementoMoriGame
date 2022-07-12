using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryPlatform : Collideable
{
    [SerializeField] float dissapearTime;
    [SerializeField] float appearTime;
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            if (coll is CapsuleCollider2D)
            {
                StartCoroutine("dissapearHandler");
            }

        }
    }

    IEnumerator dissapearHandler()
    {
        yield return new WaitForSeconds(dissapearTime);
        //insira aqui animação de desaparecer
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false; // quando tiver animação essa linha n precisa mais existir
        funcaoTemporaria(false); // deletar depois que não for mais placeholder
        yield return new WaitForSeconds(appearTime);
        //insira aqui a animação de reaparecer
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true; // quando tiver animação essa linha n precisa mais existir
        funcaoTemporaria(true); // deletar depois que não for mais placeholder
    }

    private void funcaoTemporaria(bool appear) // essa função só existe pq é tudo placeholder ainda, ela tem que ser deletada depois
    {
        SpriteRenderer[] spriteChilds = new SpriteRenderer[3];
        for (int i = 0; i < 3; i++)
        {
            spriteChilds[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
            spriteChilds[i].enabled = appear;
        }

    }
}
