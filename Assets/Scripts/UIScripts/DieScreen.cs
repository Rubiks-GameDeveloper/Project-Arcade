using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIScripts
{
    public class DieScreen : MonoBehaviour
    {
        public void RestartLevel()
        {
            Time.timeScale = 1;
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }

        public void MainMenuExit()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
}
