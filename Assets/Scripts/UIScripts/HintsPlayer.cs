using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsPlayer : MonoBehaviour
{
    public GameObject text;

    void Start()
    {
        text.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        text.SetActive(true);
    }

    void OnTriggerExit2D (Collider2D col)
    { 
        text.SetActive (false); 
    }
}
