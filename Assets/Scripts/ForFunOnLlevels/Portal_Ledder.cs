using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Ledder : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private bool isFirst;
    [SerializeField] private float distance = 0.3f;
    void Start()
    {
        if (isFirst == false)
        {
            destination =  GameObject.Find("1Portal").GetComponent<Transform>();
        }
        else 
        {
            destination =  GameObject.Find("2Portal").GetComponent<Transform>();
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
