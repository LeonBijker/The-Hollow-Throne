using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void LoadGame()
    {
        // Implement load game functionality here
    }
    public void opensettings()
    {
        // Implement settings functionality here
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
