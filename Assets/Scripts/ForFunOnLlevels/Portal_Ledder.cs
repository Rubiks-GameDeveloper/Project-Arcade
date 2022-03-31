using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Ledder : MonoBehaviour
{
    private Transform destination;
    [SerializeField] private bool isFirst;
    [SerializeField] private float distance = 0.3f;
    [SerializeField] private GameObject text1;
    [SerializeField] private GameObject text2;
    void Start()
    {
        if (isFirst == false)
        {
            destination =  text1.GetComponent<Transform>();
        }
        else 
        {
            destination =  text2.GetComponent<Transform>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Vector2.Distance(transform.position, other.transform.position) > distance)
        {
            other.transform.position = new Vector2(destination.position.x, destination.position.y);
        }
    }
}
