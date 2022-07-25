using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    private Material _material;
    private float _fade = 1f;
    public static bool IsDissolving;
    void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }
   
    void Update()
    {
        if (IsDissolving)
        {
            _fade -= Time.deltaTime;

            if (_fade <= 0f)
            {
                _fade = 0;
                IsDissolving = false;
            }
            
            _material.SetFloat("_Fade", _fade);
        }
    }
}
