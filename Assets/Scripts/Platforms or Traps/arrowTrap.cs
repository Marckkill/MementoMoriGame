using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowTrap : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    private Transform arrowSpawnPoint;
    [SerializeField] Vector2 arrowSpeed;
    [SerializeField] float interval;
    private bool activated;
    private GameObject arrows;
    void Start()
    {
        arrowSpawnPoint = transform.GetChild(0);
        activated = true;
    }

    // Update is called once per frame 
    void FixedUpdate()
    {
        // no futuro adicionar um script pra que isso só se ative caso o player esteja proximo
        if (activated)
        {
            arrows = Instantiate(arrowPrefab, arrowSpawnPoint);
            arrows.GetComponent<Rigidbody2D>().velocity = arrowSpeed * Time.deltaTime;

            StartCoroutine("ArrowHandler");
        }
    }

    IEnumerator ArrowHandler()
    {
        activated = false;
        yield return new WaitForSeconds(interval);
        activated = true;
    }
}
