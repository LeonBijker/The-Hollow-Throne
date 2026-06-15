using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // ensure timeScale is restored in case we returned from a paused state
        UnityEngine.Time.timeScale = 1f;
        UnityEngine.Time.fixedDeltaTime = 0.02f;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartGame();
            return;
        }

        // Fallback if GameManager is not present
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void LoadGame()
    {
        // Implement load game functionality here
    }

    public void opensettings()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
