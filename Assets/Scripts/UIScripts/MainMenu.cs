using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIScripts
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void ExitGame()
        {
            Debug.Log("Игра закончилась хэ");
            Application.Quit();
        }
    }
}

