using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState State { get; private set; } = GameState.MainMenu;

    public event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        SetState(GameState.Playing);
        // ensure scene 2 is loaded for gameplay
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void EndGame()
    {
        SetState(GameState.Playing);
        {
        SetState(GameState.GameOver);
    }
    }

    public void ReturnToMainMenu()
    {
        SetState(GameState.MainMenu);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void SetState(GameState newState)
    {
        if (State == newState) return;
        State = newState;
        OnGameStateChanged?.Invoke(State);
    }
}
