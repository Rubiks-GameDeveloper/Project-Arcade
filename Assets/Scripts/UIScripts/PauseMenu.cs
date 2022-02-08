using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIScripts
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private bool pauseGame;
        [SerializeField] private GameObject pauseGameMenu;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseGame)
                    Resume();
                else
                    Pause();
            }
        }


        public void Resume()
        {
            pauseGameMenu.SetActive(false);
            Time.timeScale = 1f;
            pauseGame = false;
        }
        public void Pause()
        {
            pauseGameMenu.SetActive(true);
            Time.timeScale = 0f;
            pauseGame = true;
        }
        public void LoadMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        }
    }
}
