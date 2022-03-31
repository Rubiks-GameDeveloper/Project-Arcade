using System;
using UnityEngine;

public class JumpStopper : MonoBehaviour
{
    [SerializeField] private PlayerProgrammingTransformer player;

    private void OnCollisionEnter2D(Collision2D col)
    {
        player.JumpStopper(col);
    }
}
