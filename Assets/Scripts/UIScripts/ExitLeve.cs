using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLeve : MonoBehaviour
{
    [SerializeField] private int nextLevelIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(nextLevelIndex);
    }

}
