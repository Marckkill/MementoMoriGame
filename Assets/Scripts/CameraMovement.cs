using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform followPoint;
    [SerializeField] private float adjustX;
    [SerializeField] private float adjustY;
    void Start()
    {
        followPoint = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(followPoint.position.x + adjustX, followPoint.position.y + adjustY, gameObject.transform.position.z);
    }
}
