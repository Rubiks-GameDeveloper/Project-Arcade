using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Ledder : MonoBehaviour
{
    [SerializeField] private GameObject  house;
    [SerializeField] private GameObject  houseinside;
    void Start()
    {
        house.SetActive(true);
        houseinside.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        house.SetActive(false);
        houseinside.SetActive(true);
    }
    void OnTriggerExit2D(Collider2D other)
    { 
        house.SetActive(true);
        houseinside.SetActive(false);
    }
}
