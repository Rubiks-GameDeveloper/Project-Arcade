using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIScripts
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}

